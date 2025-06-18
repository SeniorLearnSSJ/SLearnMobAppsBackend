using SeniorLearnApi.Enums;

namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model containing complete user profile information
/// </summary>
/// <remarks>
/// Returned when retrieving or updating user profile information. Includes personal details,
/// membership information, role, and a list of the user's own member bulletins.
/// </remarks>
/// <example>
/// {
///   "id": "64a7b8c9d1234567890abcde",
///   "username": "johnsmith",
///   "firstName": "John",
///   "lastName": "Smith",
///   "email": "john.smith@email.com",
///   "role": "Member",
///   "membershipDate": "2024-01-15T00:00:00Z",
///   "myBulletins": [...]
/// }
/// </example>
public class ProfileResponse
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    /// <value>MongoDB ObjectId as string, used for all user-related operations</value>
    /// <example>64a7b8c9d1234567890abcde</example>
    public string Id { get; set; }

    /// <summary>
    /// User's unique username
    /// </summary>
    /// <value>Username chosen during registration, cannot be changed</value>
    /// <example>johnsmith</example>
    public string Username { get; set; }

    /// <summary>
    /// User's first name
    /// </summary>
    /// <value>Personal first name, can be updated through profile settings</value>
    /// <example>John</example>
    public string FirstName { get; set; }

    /// <summary>
    /// User's last name
    /// </summary>
    /// <value>Personal last name, can be updated through profile settings</value>
    /// <example>Smith</example>
    public string LastName { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    /// <value>Email address, must be unique across the system</value>
    /// <example>john.smith@email.com</example>
    public string Email { get; set; }

    /// <summary>
    /// User's role in the SeniorLearn system
    /// </summary>
    /// <value>Either Member for regular users or Administrator for admin users</value>
    /// <example>Member</example>
    public UserRole Role { get; set; }

    /// <summary>
    /// Date when the user joined SeniorLearn
    /// </summary>
    /// <value>UTC datetime indicating when the user account was created</value>
    /// <example>2024-01-15T00:00:00Z</example>
    public DateTime MembershipDate { get; set; }

    /// <summary>
    /// List of member bulletins created by this user
    /// </summary>
    /// <value>Collection of bulletin list items showing all bulletins authored by this user</value>
    public List<MemberBulletinListItemResponse> MyBulletins { get; set; }
}
