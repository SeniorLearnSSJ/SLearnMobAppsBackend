namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Base class for all bulletin response DTOs containing common properties
/// </summary>
/// <remarks>
/// Provides shared properties for both official and member bulletins including identification,
/// authorship information, and timestamp metadata. This ensures consistency across bulletin types.
/// </remarks>
public abstract class BulletinResponseBase
{
    /// <summary>
    /// Unique identifier for the bulletin
    /// </summary>
    /// <value>MongoDB ObjectId as string, used for all bulletin operations</value>
    /// <example>64a7b8c9d1234567890abcde</example>
    public string Id { get; set; }

    /// <summary>
    /// Title of the bulletin
    /// </summary>
    /// <value>Descriptive title summarizing the bulletin content</value>
    /// <example>Weekly Bridge Club Meeting</example>
    public string Title { get; set; }

    /// <summary>
    /// User ID of the bulletin creator
    /// </summary>
    /// <value>MongoDB ObjectId of the user who created this bulletin</value>
    /// <example>64a7b8c9d1234567890abcdf</example>
    public string CreatedById { get; set; }

    /// <summary>
    /// Username of the bulletin creator
    /// </summary>
    /// <value>Display name of the user who created this bulletin</value>
    /// <example>johnsmith</example>
    public string CreatedByUsername { get; set; }

    /// <summary>
    /// Timestamp when the bulletin was created
    /// </summary>
    /// <value>UTC datetime indicating when the bulletin was first created</value>
    /// <example>2024-06-13T10:30:00Z</example>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the bulletin was last updated
    /// </summary>
    /// <value>UTC datetime indicating the last modification, null if never updated</value>
    /// <example>2024-06-13T15:45:00Z</example>
    public DateTime? UpdatedAt { get; set; }
}