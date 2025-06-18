namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model containing JWT authentication tokens and user role information
/// </summary>
/// <remarks>
/// Returned after successful authentication (sign-in) or registration. Contains all necessary
/// tokens for accessing protected endpoints and information about token expiration.
/// </remarks>
/// <example>
/// {
///   "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
///   "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
///   "expiresIn": 3600,
///   "role": "Member"
/// }
/// </example>
public class AuthResponse
{
    /// <summary>
    /// JWT access token for authenticating API requests
    /// </summary>
    /// <value>Bearer token to be included in Authorization header for protected endpoints</value>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGE3YjhjOWQxMjM0NTY3ODkwYWJjZGUi...</example>
    public string AccessToken { get; set; }

    /// <summary>
    /// JWT refresh token for obtaining new access tokens
    /// </summary>
    /// <value>Long-lived token used to refresh expired access tokens without re-authentication</value>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGE3YjhjOWQxMjM0NTY3ODkwYWJjZGUi...</example>
    public string RefreshToken { get; set; }

    /// <summary>
    /// Access token expiration time in seconds
    /// </summary>
    /// <value>Number of seconds until the access token expires, typically 3600 (1 hour)</value>
    /// <example>3600</example>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// User's role in the SeniorLearn system
    /// </summary>
    /// <value>Either "Member" for regular users or "Administrator" for admin users</value>
    /// <example>Member</example>
    public string Role { get; set; }
}
