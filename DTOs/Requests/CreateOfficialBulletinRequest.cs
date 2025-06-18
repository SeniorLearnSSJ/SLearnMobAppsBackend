using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for creating a new official SeniorLearn bulletin
/// </summary>
/// <remarks>
/// Official bulletins are administrative announcements and important information from the SeniorLearn organization.
/// Only users with Administrator role can create official bulletins. These are visible to all users including anonymous visitors.
/// </remarks>
/// <example>
/// {
///   "title": "New Community Guidelines",
///   "content": "We are implementing new community guidelines effective next month."
/// }
/// </example>
public class CreateOfficialBulletinRequest
{
    /// <summary>
    /// The title of the official bulletin
    /// </summary>
    /// <value>A clear, authoritative title for official announcements</value>
    /// <example>New Community Guidelines</example>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// The main content body of the official bulletin
    /// </summary>
    /// <value>Comprehensive information about official announcements, policies, or important updates</value>
    /// <example>We are implementing new community guidelines effective next month.</example>
    [Required]
    public string Content { get; set; }
}
