namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Base class for bulletin detail responses (full view)
/// </summary>
/// <remarks>
/// Used for individual bulletin retrieval where complete information is required.
/// Includes full content in addition to all common bulletin properties.
/// </remarks>
public abstract class BulletinDetailResponseBase : BulletinResponseBase
{
    /// <summary>
    /// Full content body of the bulletin
    /// </summary>
    /// <value>Complete text content of the bulletin</value>
    /// <example>Join us every Wednesday at 2 PM in the community center for our weekly bridge games. All skill levels welcome! We provide cards and refreshments.</example>
    public string Content { get; set; }
}