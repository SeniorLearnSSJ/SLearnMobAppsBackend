using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for user sign out (token revocation)
/// </summary>
/// <remarks>
/// Revokes the specified refresh token to complete the sign-out process and prevent further token refreshing.
/// The refresh token must belong to the currently authenticated user.
/// </remarks>
/// <example>
/// {
///   "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGE3YjhjOWQxMjM0NTY3ODkwYWJjZGUi..."
/// }
/// </example>
public class SignOutRequest
{
    /// <summary>
    /// The refresh token to revoke during sign out
    /// </summary>
    /// <value>Must be a valid refresh token belonging to the current user</value>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGE3YjhjOWQxMjM0NTY3ODkwYWJjZGUi...</example>
    [Required]
    public string RefreshToken { get; set; }
}
