namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Response model containing user preference settings
/// </summary>
/// <remarks>
/// Returned when retrieving or updating user settings. Contains accessibility and personalization
/// preferences that affect how the mobile application displays content and behaves.
/// </remarks>
/// <example>
/// {
///   "fontSize": 24,
///   "darkMode": false,
///   "enableNotifications": true
/// }
/// </example>
public class SettingsResponse
{
    /// <summary>
    /// User's preferred font size for text display
    /// </summary>
    /// <value>Font size in points for accessibility needs</value>
    /// <example>24</example>
    public int FontSize { get; set; }

    /// <summary>
    /// User's preference for dark mode theme
    /// </summary>
    /// <value>True if dark mode is enabled, false for light mode</value>
    /// <example>false</example>
    public bool DarkMode { get; set; }

    /// <summary>
    /// User's notification preference setting
    /// </summary>
    /// <value>True if push notifications are enabled, false if disabled</value>
    /// <example>true</example>
    public bool EnableNotifications { get; set; }

}
