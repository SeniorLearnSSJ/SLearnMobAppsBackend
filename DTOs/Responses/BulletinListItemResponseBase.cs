namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Base class for bulletin list item responses (summary view)
/// </summary>
/// <remarks>
/// Used for bulletin listing endpoints where full content is not needed. Inherits all
/// common bulletin properties but excludes detailed content to optimize response size.
/// </remarks>
public abstract class BulletinListItemResponseBase : BulletinResponseBase
{
}