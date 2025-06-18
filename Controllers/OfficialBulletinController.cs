using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearnApi.DTOs.Requests;
using SeniorLearnApi.DTOs.Responses;
using SeniorLearnApi.Services;

namespace SeniorLearnApi.Controllers;

/// <summary>
/// Manages official SeniorLearn bulletins created by administrators
/// </summary>
/// <remarks>
/// Provides access to official bulletins for all users (including anonymous), but restricts creation, 
/// modification, and deletion to administrators only. Official bulletins contain important announcements
/// and information from the SeniorLearn organization.
/// </remarks>
[ApiController]
[Route("api/bulletins/official")]
public class OfficialBulletinController : ControllerBase
{
    private readonly UserContextService _userContextService;
    private readonly OfficialBulletinService _officialBulletinService;

    public OfficialBulletinController(UserContextService userContextService, OfficialBulletinService officialBulletinService)
    {
        _userContextService = userContextService;
        _officialBulletinService = officialBulletinService;
    }

    /// <summary>
    /// Retrieves all official bulletins
    /// </summary>
    /// <returns>List of all official bulletins sorted by creation date (newest first)</returns>
    /// <response code="200">Bulletins successfully retrieved</response>
    /// <remarks>
    /// This endpoint is accessible to anonymous users, allowing everyone to view official SeniorLearn announcements
    /// without requiring authentication.
    /// </remarks>
    /// <example>
    /// GET /api/bulletins/official
    /// </example>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<List<OfficialBulletinListItemResponse>>>> GetAllOfficialBulletins()
    {
        var response = await _officialBulletinService.GetAllOfficialBulletinsAsync();
        return Ok(ApiResponse<List<OfficialBulletinListItemResponse>>.SuccessResponse(response));
    }

    /// <summary>
    /// Retrieves a specific official bulletin by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the official bulletin</param>
    /// <returns>Complete official bulletin details including title, content, author, and timestamps</returns>
    /// <response code="200">Bulletin successfully retrieved</response>
    /// <response code="404">Bulletin not found or invalid ID format</response>
    /// <remarks>
    /// This endpoint is accessible to anonymous users, allowing everyone to read full official bulletin content
    /// without authentication requirements.
    /// </remarks>
    /// <example>
    /// GET /api/bulletins/official/64a7b8c9d1234567890abcde
    /// </example>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<OfficialBulletinDetailResponse>>> GetOfficialBulletinById(string id)
    {
        var response = await _officialBulletinService.GetOfficialBulletinByIdAsync(id);
        if (response == null)
        {
            return NotFound(ApiResponse<OfficialBulletinDetailResponse>.ErrorResponse("Bulletin not found"));
        }
        return Ok(ApiResponse<OfficialBulletinDetailResponse>.SuccessResponse(response));
    }

    /// <summary>
    /// Creates a new official bulletin
    /// </summary>
    /// <param name="request">Bulletin details including title and content</param>
    /// <returns>The newly created official bulletin with complete details and generated ID</returns>
    /// <response code="201">Bulletin successfully created</response>
    /// <response code="400">Invalid request data or validation errors</response>
    /// <response code="401">User not authenticated or not authorized</response>
    /// <response code="403">User does not have administrator privileges</response>
    /// <remarks>
    /// This endpoint requires administrator authentication. Only users with the "Administrator" role
    /// can create official bulletins.
    /// </remarks>
    /// <example>
    /// POST /api/bulletins/official
    /// Authorization: Bearer {jwt-token}
    /// {
    ///   "title": "Important: New Community Guidelines",
    ///   "content": "We are implementing new community guidelines effective next month. Please review the attached document for details..."
    /// }
    /// </example>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ApiResponse<OfficialBulletinDetailResponse>>> CreateOfficialBulletin([FromBody] CreateOfficialBulletinRequest request)
    {
        var adminId = _userContextService.GetUserId();
        var adminName = _userContextService.GetUsername();
        if (string.IsNullOrEmpty(adminId) || string.IsNullOrEmpty(adminName))
        {
            return Unauthorized(ApiResponse<OfficialBulletinDetailResponse>.ErrorResponse("Unable to identify current user"));
        }

        var response = await _officialBulletinService.CreateOfficialBulletinAsync(request, adminId, adminName);
        return StatusCode(201, ApiResponse<OfficialBulletinDetailResponse>.SuccessResponse(response, "Official bulletin created successfully"));
    }

    /// <summary>
    /// Updates an existing official bulletin
    /// </summary>
    /// <param name="id">The unique identifier of the bulletin to update</param>
    /// <param name="request">Updated bulletin details including title and content</param>
    /// <returns>The updated bulletin with new information and updated timestamp</returns>
    /// <response code="200">Bulletin successfully updated</response>
    /// <response code="400">Invalid request data</response>
    /// <response code="401">User not authenticated or not authorized</response>
    /// <response code="403">User does not have administrator privileges</response>
    /// <response code="404">Bulletin not found</response>
    /// <remarks>
    /// This endpoint requires administrator authentication. Only users with the "Administrator" role
    /// can update official bulletins.
    /// </remarks>
    /// <example>
    /// PUT /api/bulletins/official/64a7b8c9d1234567890abcde
    /// Authorization: Bearer {jwt-token}
    /// {
    ///   "title": "Updated: New Community Guidelines",
    ///   "content": "We are implementing updated community guidelines effective next month. The implementation date has been moved to accommodate feedback..."
    /// }
    /// </example>
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ApiResponse<OfficialBulletinDetailResponse>>> UpdateOfficialBulletin(string id, [FromBody] UpdateOfficialBulletinRequest request)
    {
        var currentUserId = _userContextService.GetUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(ApiResponse<OfficialBulletinDetailResponse>.ErrorResponse("Unable to identify current user"));
        }

        var response = await _officialBulletinService.UpdateOfficialBulletinAsync(id, request);
        if (response == null)
        {
            return NotFound(ApiResponse<OfficialBulletinDetailResponse>.ErrorResponse("Bulletin not found"));
        }
        return Ok(ApiResponse<OfficialBulletinDetailResponse>.SuccessResponse(response, "Bulletin updated successfully"));
    }

    /// <summary>
    /// Deletes an official bulletin
    /// </summary>
    /// <param name="id">The unique identifier of the bulletin to delete</param>
    /// <returns>No content on successful deletion</returns>
    /// <response code="204">Bulletin successfully deleted</response>
    /// <response code="401">User not authenticated or not authorized</response>
    /// <response code="403">User does not have administrator privileges</response>
    /// <response code="404">Bulletin not found</response>
    /// <remarks>
    /// This endpoint requires administrator authentication. Only users with the "Administrator" role
    /// can delete official bulletins.
    /// </remarks>
    /// <example>
    /// DELETE /api/bulletins/official/64a7b8c9d1234567890abcde
    /// Authorization: Bearer {jwt-token}
    /// </example>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> DeleteOfficialBulletin(string id)
    {
        var currentUserId = _userContextService.GetUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(ApiResponse<bool>.ErrorResponse("Unable to identify current user"));
        }

        var result = await _officialBulletinService.DeleteOfficialBulletinAsync(id);
        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Bulletin not found"));
        }

        return NoContent();
    }
}
