using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearnApi.DTOs.Requests;
using SeniorLearnApi.DTOs.Responses;
using SeniorLearnApi.Services;

namespace SeniorLearnApi.Controllers;

/// <summary>
/// Handles authentication operations for the SeniorLearn bulletin system
/// </summary>
/// <remarks>
/// Provides endpoints for user registration, sign-in, token refresh, and sign-out functionality.
/// Supports JWT-based authentication with refresh token mechanism for secure access.
/// </remarks>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserContextService _userContextService;
    private readonly AuthService _authService;

    public AuthController(UserContextService userContextService, AuthService authService)
    {
        _userContextService = userContextService;
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user account
    /// </summary>
    /// <param name="request">User registration details including username, email, password, and personal information</param>
    /// <returns>Authentication response with access token and user role information</returns>
    /// <response code="201">User successfully registered and authenticated</response>
    /// <response code="400">Invalid request data or validation errors</response>
    /// <response code="409">Username or email already exists</response>
    /// <example>
    /// POST /api/auth/register
    /// {
    ///   "username": "johnsmith",
    ///   "email": "john.smith@email.com",
    ///   "password": "SecurePassword123!",
    ///   "firstName": "John",
    ///   "lastName": "Smith"
    /// }
    /// </example>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(request);
        if (user == null)
        {
            return Conflict(ApiResponse<AuthResponse>.ErrorResponse("Username or email already exists"));
        }

        var authResponse = await _authService.CreateAuthResponseAsync(user);
        return StatusCode(201, ApiResponse<AuthResponse>.SuccessResponse(authResponse, "Registration successful"));
    }

    /// <summary>
    /// Authenticates a user with username and password
    /// </summary>
    /// <param name="request">Sign-in credentials containing username and password</param>
    /// <returns>Authentication response with access token, refresh token, and user role</returns>
    /// <response code="200">Successfully authenticated</response>
    /// <response code="400">Invalid request format</response>
    /// <response code="401">Invalid username or password</response>
    /// <example>
    /// POST /api/auth/sign-in
    /// {
    ///   "username": "Peter",
    ///   "password": "user123"
    /// }
    /// </example>
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> SignIn([FromBody] SignInRequest request)
    {
        var user = await _authService.SignInAsync(request);
        if (user == null)
        {
            return Unauthorized(ApiResponse<AuthResponse>.ErrorResponse("Invalid username or password"));
        }

        var authResponse = await _authService.CreateAuthResponseAsync(user);
        return Ok(ApiResponse<AuthResponse>.SuccessResponse(authResponse, "Sign In successful"));
    }

    /// <summary>
    /// Refreshes an expired access token using a valid refresh token
    /// </summary>
    /// <param name="request">Refresh token request containing the current refresh token</param>
    /// <returns>New authentication response with fresh access token and refresh token</returns>
    /// <response code="200">Token successfully refreshed</response>
    /// <response code="400">Invalid request format</response>
    /// <response code="401">Invalid or expired refresh token</response>
    /// <example>
    /// POST /api/auth/refresh-token
    /// {
    ///   "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    /// }
    /// </example>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var authResponse = await _authService.RefreshTokenAsync(request);
        if (authResponse == null)
        {
            return Unauthorized(ApiResponse<AuthResponse>.ErrorResponse("Invalid or expired refresh token"));
        }
        return Ok(ApiResponse<AuthResponse>.SuccessResponse(authResponse, "Token refreshed successfully"));
    }

    /// <summary>
    /// Signs out a user by revoking their refresh token
    /// </summary>
    /// <param name="request">Sign-out request containing the refresh token to revoke</param>
    /// <returns>Confirmation of successful sign-out</returns>
    /// <response code="200">Successfully signed out</response>
    /// <response code="400">Invalid refresh token or token does not belong to user</response>
    /// <response code="401">User not authenticated or unable to identify current user</response>
    /// <example>
    /// POST /api/auth/sign-out
    /// {
    ///   "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    /// }
    /// </example>
    [HttpPost("sign-out")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> SignOut([FromBody] SignOutRequest request)
    {
        var currentUserId = _userContextService.GetUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(ApiResponse<bool>.ErrorResponse("Unable to identify current user"));
        }

        var result = await _authService.SignOutAsync(currentUserId, request.RefreshToken);
        if (!result)
        {
            return BadRequest(ApiResponse<bool>.ErrorResponse("Invalid refresh token or token does not belong to user"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Sign out successful"));
    }
}
