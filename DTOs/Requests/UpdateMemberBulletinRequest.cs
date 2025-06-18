using SeniorLearnApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for updating an existing member bulletin
/// </summary>
/// <remarks>
/// Allows members to modify their own bulletins or administrators to moderate any member bulletin.
/// All fields are required and will replace the existing bulletin content completely.
/// </remarks>
/// <example>
/// {
///   "title": "Updated: Weekly Bridge Club Meeting",
///   "content": "Join us every Wednesday at 3 PM (time changed!) in the community center for bridge games.",
///   "category": "Event"
/// }
/// </example>
public class UpdateMemberBulletinRequest
{
    /// <summary>
    /// Updated title for the member bulletin
    /// </summary>
    /// <value>New title that will replace the existing bulletin title</value>
    /// <example>Updated: Weekly Bridge Club Meeting</example>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// Updated content for the member bulletin
    /// </summary>
    /// <value>New content that will replace the existing bulletin content</value>
    /// <example>Join us every Wednesday at 3 PM (time changed!) in the community center for bridge games.</example>
    [Required]
    public string Content { get; set; }

    /// <summary>
    /// Updated category for the member bulletin
    /// </summary>
    /// <value>New category classification that will replace the existing category</value>
    /// <example>Event</example>
    public MemberBulletinCategory Category { get; set; }
}
