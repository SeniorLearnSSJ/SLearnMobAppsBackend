using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for user authentication (sign in)
/// </summary>
/// <remarks>
/// Used to authenticate existing users and obtain JWT tokens for API access.
/// Credentials are validated against stored user information.
/// </remarks>
/// <example>
/// {
///   "username": "johnsmith",
///   "password": "SecurePassword123!"
/// }
/// </example>
public class SignInRequest
{
    /// <summary>
    /// Username for authentication
    /// </summary>
    /// <value>Must match an existing user account in the system</value>
    /// <example>johnsmith</example>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Password for authentication
    /// </summary>
    /// <value>Must match the password associated with the specified username</value>
    /// <example>SecurePassword123!</example>
    [Required]
    public string Password { get; set; }
}
