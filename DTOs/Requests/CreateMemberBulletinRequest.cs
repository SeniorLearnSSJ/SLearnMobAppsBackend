using SeniorLearnApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for creating a new member bulletin in the SeniorLearn bulletin system
/// </summary>
/// <remarks>
/// Member bulletins are user-generated content that can be categorized as Interests, Events, or Updates.
/// All authenticated members can create bulletins to share information with the community.
/// </remarks>
/// <example>
/// {
///   "title": "Weekly Bridge Club Meeting",
///   "content": "Join us every Wednesday at 2 PM in the community center for bridge games. All skill levels welcome!",
///   "category": "Event"
/// }
/// </example>
public class CreateMemberBulletinRequest
{
    /// <summary>
    /// The title of the member bulletin
    /// </summary>
    /// <value>A descriptive title that summarizes the bulletin content</value>
    /// <example>Weekly Bridge Club Meeting</example>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// The main content body of the member bulletin
    /// </summary>
    /// <value>Detailed information about the bulletin topic</value>
    /// <example>Join us every Wednesday at 2 PM in the community center for bridge games. All skill levels welcome!</example>
    [Required]
    public string Content { get; set; }

    /// <summary>
    /// The category classification for the member bulletin
    /// </summary>
    /// <value>Must be one of: Interest, Event, or Update to help users filter and browse relevant content</value>
    /// <example>Event</example>
    public MemberBulletinCategory Category { get; set; }
}
