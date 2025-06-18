using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearnApi.DTOs.Requests;
using SeniorLearnApi.DTOs.Responses;
using SeniorLearnApi.Services;

namespace SeniorLearnApi.Controllers;

/// <summary>
/// Manages user profile information and settings for authenticated members
/// </summary>
/// <remarks>
/// Provides endpoints for viewing and updating user profiles, including personal information,
/// membership details, user bulletins, and preference settings like font size, dark mode, and notifications.
/// All endpoints require authentication.
/// </remarks>
[ApiController]
[Route("api/profile")]
public class UserProfileController : ControllerBase
{
    private readonly UserContextService _userContextService;
    private readonly UserProfileService _userProfileService;
    private readonly UserSettingService _userSettingService;

    public UserProfileController(UserContextService userContextService, UserProfileService userProfileService, UserSettingService userSettingService)
    {
        _userContextService = userContextService;
        _userProfileService = userProfileService;
        _userSettingService = userSettingService;
    }

    /// <summary>
    /// Retrieves the current user's profile information
    /// </summary>
    /// <returns>Complete user profile including personal details, membership date, and list of user's bulletins</returns>
    /// <response code="200">Profile successfully retrieved</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">User profile not found</response>
    /// <example>
    /// GET /api/profile
    /// Authorization: Bearer {jwt-token}
    /// </example>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResponse<ProfileResponse>>> GetProfile()
    {
        var currentUserId = _userContextService.GetUserId();
        var response = await _userProfileService.GetUserProfileAsync(currentUserId);

        if (response == null)
        {
            return NotFound(ApiResponse<ProfileResponse>.ErrorResponse("User not found"));
        }

        return Ok(ApiResponse<ProfileResponse>.SuccessResponse(response));
    }

    /// <summary>
    /// Updates the current user's profile information
    /// </summary>
    /// <param name="request">Updated profile information including first name, last name, and email</param>
    /// <returns>Updated user profile with the new information</returns>
    /// <response code="200">Profile successfully updated</response>
    /// <response code="400">Invalid request data</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">User profile not found</response>
    /// <example>
    /// PUT /api/profile
    /// Authorization: Bearer {jwt-token}
    /// {
    ///   "firstName": "John",
    ///   "lastName": "Smith",
    ///   "email": "john.smith@newemail.com"
    /// }
    /// </example>
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<ApiResponse<ProfileResponse>>> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var currentUserId = _userContextService.GetUserId();
        var response = await _userProfileService.UpdateUserProfileAsync(currentUserId, request);

        if (response == null)
        {
            return NotFound(ApiResponse<ProfileResponse>.ErrorResponse("User not found"));
        }

        return Ok(ApiResponse<ProfileResponse>.SuccessResponse(response, "Profile updated successfully"));
    }

    /// <summary>
    /// Retrieves the current user's preference settings
    /// </summary>
    /// <returns>User settings including font size, dark mode, and notification preferences</returns>
    /// <response code="200">Settings successfully retrieved</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">User settings not found</response>
    /// <example>
    /// GET /api/profile/settings
    /// Authorization: Bearer {jwt-token}
    /// </example>
    [HttpGet("settings")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<SettingsResponse>>> GetSettings()
    {
        var currentUserId = _userContextService.GetUserId();
        var response = await _userSettingService.GetUserSettingsAsync(currentUserId);

        if (response == null)
        {
            return NotFound(ApiResponse<SettingsResponse>.ErrorResponse("User settings not found"));
        }

        return Ok(ApiResponse<SettingsResponse>.SuccessResponse(response));
    }

    /// <summary>
    /// Updates the current user's preference settings
    /// </summary>
    /// <param name="request">Updated settings including font size, dark mode preference, and notification settings</param>
    /// <returns>Updated user settings with the new preferences</returns>
    /// <response code="200">Settings successfully updated</response>
    /// <response code="400">Invalid request data</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">User settings not found</response>
    /// <example>
    /// PUT /api/profile/settings
    /// Authorization: Bearer {jwt-token}
    /// {
    ///   "fontSize": 36,
    ///   "darkMode": true,
    ///   "enableNotifications": false
    /// }
    /// </example>
    [HttpPut("settings")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<SettingsResponse>>> UpdateSettings([FromBody] UpdateSettingsRequest request)
    {
        var currentUserId = _userContextService.GetUserId();
        var response = await _userSettingService.UpdateUserSettingsAsync(currentUserId, request);

        if (response == null)
        {
            return NotFound(ApiResponse<SettingsResponse>.ErrorResponse("User not found"));
        }

        return Ok(ApiResponse<SettingsResponse>.SuccessResponse(response, "Settings updated successfully"));
    }
}
