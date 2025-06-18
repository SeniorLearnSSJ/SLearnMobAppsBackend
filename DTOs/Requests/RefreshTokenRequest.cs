using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for refreshing an expired JWT access token
/// </summary>
/// <remarks>
/// Used in the JWT authentication flow to obtain a new access token when the current one expires.
/// The refresh token must be valid and not expired to successfully generate new tokens.
/// </remarks>
/// <example>
/// {
///   "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGE3YjhjOWQxMjM0NTY3ODkwYWJjZGUi..."
/// }
/// </example>
public class RefreshTokenRequest
{
    /// <summary>
    /// The refresh token used to generate a new access token
    /// </summary>
    /// <value>A valid JWT refresh token that was previously issued during authentication</value>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGE3YjhjOWQxMjM0NTY3ODkwYWJjZGUi...</example>
    [Required]
    public string RefreshToken { get; set; }
}
