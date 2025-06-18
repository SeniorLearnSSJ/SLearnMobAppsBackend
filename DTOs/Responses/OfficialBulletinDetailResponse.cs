namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model for detailed official bulletin information
/// </summary>
/// <remarks>
/// Used when retrieving a specific official bulletin by ID, providing complete information
/// including full content. Accessible to all users including anonymous visitors.
/// </remarks>
/// <example>
/// {
///   "id": "64a7b8c9d1234567890abcde",
///   "title": "Important: New Community Guidelines",
///   "content": "We are implementing new community guidelines effective next month...",
///   "createdById": "64a7b8c9d1234567890abcdf",
///   "createdByUsername": "admin",
///   "createdAt": "2024-06-13T10:30:00Z",
///   "updatedAt": "2024-06-13T15:45:00Z"
/// }
/// </example>
public class OfficialBulletinDetailResponse : BulletinDetailResponseBase
{
}