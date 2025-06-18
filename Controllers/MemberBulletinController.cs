using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearnApi.DTOs.Requests;
using SeniorLearnApi.DTOs.Responses;
using SeniorLearnApi.Enums;
using SeniorLearnApi.Services;

namespace SeniorLearnApi.Controllers;

/// <summary>
/// Manages member-created bulletins categorized as Interests, Events, or Updates
/// </summary>
/// <remarks>
/// Provides full CRUD operations for member bulletins. Members can create, view, update, and delete their own bulletins.
/// Administrators have additional permissions to moderate any member bulletin. All endpoints require authentication.
/// Supports filtering by category and user ID for flexible bulletin browsing.
/// </remarks>
[ApiController]
[Route("api/bulletins/member")]
public class MemberBulletinController : ControllerBase
{
    private readonly UserContextService _userContextService;
    private readonly MemberBulletinService _memberBulletinService;

    public MemberBulletinController(UserContextService userContextService, MemberBulletinService memberBulletinService)
    {
        _userContextService = userContextService;
        _memberBulletinService = memberBulletinService;
    }

    /// <summary>
    /// Retrieves member bulletins with optional filtering by category and user
    /// </summary>
    /// <param name="category">Optional filter by bulletin category (Interests, Events, or Updates)</param>
    /// <param name="userId">Optional filter by specific user ID to show bulletins from that user only</param>
    /// <returns>List of member bulletins matching the specified filters, sorted by creation date (newest first)</returns>
    /// <response code="200">Bulletins successfully retrieved</response>
    /// <response code="401">User not authenticated</response>
    /// <example>
    /// GET /api/bulletins/member
    /// GET /api/bulletins/member?category=Event=
    /// GET /api/bulletins/member?userId=64a7b8c9d1234567890abcde
    /// GET /api/bulletins/member?category=Interest&amp;userId=64a7b8c9d1234567890abcde
    /// Authorization: Bearer {jwt-token}
    /// </example>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<MemberBulletinListItemResponse>>>> GetAllMemberBulletins(
    [FromQuery] MemberBulletinCategory? category = null,
    [FromQuery] string? userId = null)
    {
        var response = await _memberBulletinService.GetMemberBulletinsAsync(category, userId);
        return Ok(ApiResponse<List<MemberBulletinListItemResponse>>.SuccessResponse(response));
    }

    /// <summary>
    /// Retrieves a specific member bulletin by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the member bulletin</param>
    /// <returns>Complete member bulletin details including title, content, author, category, and timestamps</returns>
    /// <response code="200">Bulletin successfully retrieved</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Bulletin not found or invalid ID format</response>
    /// <example>
    /// GET /api/bulletins/member/64a7b8c9d1234567890abcde
    /// Authorization: Bearer {jwt-token}
    /// </example>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<MemberBulletinDetailResponse>>> GetMemberBulletinById(string id)
    {
        var response = await _memberBulletinService.GetMemberBulletinByIdAsync(id);
        if (response == null)
        {
            return NotFound(ApiResponse<MemberBulletinDetailResponse>.ErrorResponse("Bulletin not found"));
        }
        return Ok(ApiResponse<MemberBulletinDetailResponse>.SuccessResponse(response));
    }

    /// <summary>
    /// Creates a new member bulletin
    /// </summary>
    /// <param name="request">Bulletin details including title, content, and category (Interests, Events, or Updates)</param>
    /// <returns>The newly created bulletin with complete details and generated ID</returns>
    /// <response code="201">Bulletin successfully created</response>
    /// <response code="400">Invalid request data or validation errors</response>
    /// <response code="401">User not authenticated or unable to identify current user</response>
    /// <example>
    /// POST /api/bulletins/member
    /// Authorization: Bearer {jwt-token}
    /// {
    ///   "title": "Weekly Bridge Club Meeting",
    ///   "content": "Join us every Wednesday at 2 PM in the community center for our weekly bridge games. All skill levels welcome!",
    ///   "category": "Event"
    /// }
    /// </example>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse<MemberBulletinDetailResponse>>> CreateMemberBulletin([FromBody] CreateMemberBulletinRequest request)
    {
        var memberId = _userContextService.GetUserId();
        var memberName = _userContextService.GetUsername();
        if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(memberName))
        {
            return Unauthorized(ApiResponse<MemberBulletinDetailResponse>.ErrorResponse("Unable to identify current user"));
        }

        var response = await _memberBulletinService.CreateMemberBulletinAsync(request, memberId, memberName);
        return StatusCode(201, ApiResponse<MemberBulletinDetailResponse>.SuccessResponse(response, "Member bulletin created successfully"));
    }

    /// <summary>
    /// Updates an existing member bulletin
    /// </summary>
    /// <param name="id">The unique identifier of the bulletin to update</param>
    /// <param name="request">Updated bulletin details including title, content, and category</param>
    /// <returns>The updated bulletin with new information and updated timestamp</returns>
    /// <response code="200">Bulletin successfully updated</response>
    /// <response code="400">Invalid request data</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Bulletin not found or user doesn't have permission to update it</response>
    /// <remarks>
    /// Members can only update their own bulletins. Administrators can update any member bulletin for moderation purposes.
    /// </remarks>
    /// <example>
    /// PUT /api/bulletins/member/64a7b8c9d1234567890abcde
    /// Authorization: Bearer {jwt-token}
    /// {
    ///   "title": "Updated: Weekly Bridge Club Meeting",
    ///   "content": "Join us every Wednesday at 3 PM (time changed!) in the community center for our weekly bridge games.",
    ///   "category": "Event"
    /// }
    /// </example>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<MemberBulletinDetailResponse>>> UpdateMemberBulletin(string id, [FromBody] UpdateMemberBulletinRequest request)
    {
        var currentUserId = _userContextService.GetUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(ApiResponse<MemberBulletinDetailResponse>.ErrorResponse("Unable to identify current user"));
        }

        var isAdmin = _userContextService.IsAdmin();
        var response = await _memberBulletinService.UpdateMemberBulletinAsync(id, request, currentUserId, isAdmin);
        if (response == null)
        {
            return NotFound(ApiResponse<MemberBulletinDetailResponse>.ErrorResponse("Bulletin not found or you don't have permission to update it"));
        }
        return Ok(ApiResponse<MemberBulletinDetailResponse>.SuccessResponse(response, "Bulletin updated successfully"));
    }

    /// <summary>
    /// Deletes a member bulletin
    /// </summary>
    /// <param name="id">The unique identifier of the bulletin to delete</param>
    /// <returns>No content on successful deletion</returns>
    /// <response code="204">Bulletin successfully deleted</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Bulletin not found or user doesn't have permission to delete it</response>
    /// <remarks>
    /// Members can only delete their own bulletins. Administrators can delete any member bulletin for moderation purposes.
    /// </remarks>
    /// <example>
    /// DELETE /api/bulletins/member/64a7b8c9d1234567890abcde
    /// Authorization: Bearer {jwt-token}
    /// </example>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteMemberBulletin(string id)
    {
        var currentUserId = _userContextService.GetUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(ApiResponse<bool>.ErrorResponse("Unable to identify current user"));
        }

        var isAdmin = _userContextService.IsAdmin();
        var result = await _memberBulletinService.DeleteMemberBulletinAsync(id, currentUserId, isAdmin);
        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Bulletin not found or you don't have permission to delete it"));
        }
        return NoContent();
    }
}
