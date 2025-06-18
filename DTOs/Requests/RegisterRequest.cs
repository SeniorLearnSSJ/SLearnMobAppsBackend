using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for user registration in the SeniorLearn system
/// </summary>
/// <remarks>
/// Creates a new user account with Member role by default. All fields are required for successful registration.
/// Username and email must be unique across the system. Password must meet security requirements.
/// </remarks>
/// <example>
/// {
///   "username": "johnsmith",
///   "password": "SecurePassword123!",
///   "firstName": "John",
///   "lastName": "Smith",
///   "email": "john.smith@email.com"
/// }
/// </example>
public class RegisterRequest
{
    /// <summary>
    /// Unique username for the new user account
    /// </summary>
    /// <value>Must be unique across the system, recommended 3-30 characters, alphanumeric and underscore allowed</value>
    /// <example>johnsmith</example>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Password for the new user account
    /// </summary>
    /// <value>Must be 6-100 characters long and meet security requirements including special characters</value>
    /// <example>SecurePassword123!</example>
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    /// <summary>
    /// User's first name
    /// </summary>
    /// <value>Personal first name used for display purposes and profile information</value>
    /// <example>John</example>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// User's last name
    /// </summary>
    /// <value>Personal last name used for display purposes and profile information</value>
    /// <example>Smith</example>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    /// <value>Must be unique across the system and follow valid email format</value>
    /// <example>john.smith@email.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
