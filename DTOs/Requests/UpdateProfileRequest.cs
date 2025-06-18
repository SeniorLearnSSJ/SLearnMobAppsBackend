using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for updating user profile information
/// </summary>
/// <remarks>
/// Allows authenticated users to modify their personal information including name and email address.
/// Email must remain unique across the system. Username cannot be changed after registration.
/// </remarks>
/// <example>
/// {
///   "firstName": "John",
///   "lastName": "Smith",
///   "email": "john.smith@newemail.com"
/// }
/// </example>
public class UpdateProfileRequest
{
    /// <summary>
    /// Updated first name for the user profile
    /// </summary>
    /// <value>New first name that will replace the existing profile first name</value>
    /// <example>John</example>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Updated last name for the user profile
    /// </summary>
    /// <value>New last name that will replace the existing profile last name</value>
    /// <example>Smith</example>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Updated email address for the user profile
    /// </summary>
    /// <value>New email address that must be unique across the system and follow valid email format</value>
    /// <example>john.smith@newemail.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
