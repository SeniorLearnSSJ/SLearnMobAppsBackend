using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for updating an existing official bulletin
/// </summary>
/// <remarks>
/// Only users with Administrator role can update official bulletins.
/// All fields are required and will replace the existing bulletin content completely.
/// </remarks>
/// <example>
/// {
///   "title": "Updated: New Community Guidelines",
///   "content": "We are implementing updated community guidelines effective next month. The implementation date has been moved to accommodate feedback."
/// }
/// </example>
public class UpdateOfficialBulletinRequest
{
    /// <summary>
    /// Updated title for the official bulletin
    /// </summary>
    /// <value>New title that will replace the existing official bulletin title</value>
    /// <example>Updated: New Community Guidelines</example>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// Updated content for the official bulletin
    /// </summary>
    /// <value>New content that will replace the existing official bulletin content</value>
    /// <example>We are implementing updated community guidelines effective next month. The implementation date has been moved to accommodate feedback.</example>
    [Required]
    public string Content { get; set; }
}
