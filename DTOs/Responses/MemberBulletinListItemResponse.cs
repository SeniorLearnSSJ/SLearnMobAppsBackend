using SeniorLearnApi.Enums;

namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model for member bulletin list items
/// </summary>
/// <remarks>
/// Used in member bulletin listing endpoints to provide summary information including
/// the bulletin category for filtering and display purposes.
/// </remarks>
/// <example>
/// {
///   "id": "64a7b8c9d1234567890abcde",
///   "title": "Weekly Bridge Club Meeting",
///   "createdById": "64a7b8c9d1234567890abcdf",
///   "createdByUsername": "johnsmith",
///   "createdAt": "2024-06-13T10:30:00Z",
///   "updatedAt": null,
///   "category": "Event"
/// }
/// </example>
public class MemberBulletinListItemResponse : BulletinListItemResponseBase
{
    /// <summary>
    /// Category classification of the member bulletin
    /// </summary>
    /// <value>One of Interest, Event, or Update to help users filter relevant content</value>
    /// <example>Event</example>
    public MemberBulletinCategory Category { get; set; }
}