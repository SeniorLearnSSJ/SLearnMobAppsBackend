namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model for official bulletin list items
/// </summary>
/// <remarks>
/// Used in official bulletin listing endpoints accessible to all users including anonymous.
/// Provides summary information without full content for efficient browsing.
/// </remarks>
/// <example>
/// {
///   "id": "64a7b8c9d1234567890abcde",
///   "title": "Important: New Community Guidelines",
///   "createdById": "64a7b8c9d1234567890abcdf",
///   "createdByUsername": "admin",
///   "createdAt": "2024-06-13T10:30:00Z",
///   "updatedAt": "2024-06-13T15:45:00Z"
/// }
/// </example>
public class OfficialBulletinListItemResponse : BulletinListItemResponseBase
{
}