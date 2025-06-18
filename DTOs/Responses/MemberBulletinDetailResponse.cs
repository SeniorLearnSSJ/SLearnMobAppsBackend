using SeniorLearnApi.Enums;

namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model for detailed member bulletin information
/// </summary>
/// <remarks>
/// Used when retrieving a specific member bulletin by ID, providing complete information
/// including full content and category for display and editing purposes.
/// </remarks>
/// <example>
/// {
///   "id": "64a7b8c9d1234567890abcde",
///   "title": "Weekly Bridge Club Meeting",
///   "content": "Join us every Wednesday at 2 PM in the community center...",
///   "createdById": "64a7b8c9d1234567890abcdf",
///   "createdByUsername": "johnsmith",
///   "createdAt": "2024-06-13T10:30:00Z",
///   "updatedAt": null,
///   "category": "Event"
/// }
/// </example>
public class MemberBulletinDetailResponse : BulletinDetailResponseBase
{
    /// <summary>
    /// Category classification of the member bulletin
    /// </summary>
    /// <value>One of Interest, Event, or Update indicating the bulletin's purpose</value>
    /// <example>Event</example>
    public MemberBulletinCategory Category { get; set; }
}