using System.ComponentModel.DataAnnotations;

namespace SeniorLearnApi.DTOs.Requests;

/// <summary>
/// Request model for updating user preference settings
/// </summary>
/// <remarks>
/// Allows users to customize their application experience including accessibility preferences,
/// visual themes, and notification settings. These settings affect how the mobile app displays content.
/// </remarks>
/// <example>
/// {
///   "fontSize": 36,
///   "darkMode": true,
///   "enableNotifications": false
/// }
/// </example>
public class UpdateSettingsRequest
{
    /// <summary>
    /// Font size preference for text display
    /// </summary>
    /// <value>Font size in points for accessibility</value>
    /// <example>36</example>
    public int FontSize { get; set; }

    /// <summary>
    /// Dark mode preference for the user interface
    /// </summary>
    /// <value>True to enable dark mode theme, false for light mode theme</value>
    /// <example>true</example>
    public bool DarkMode { get; set; }

    /// <summary>
    /// Notification preference setting
    /// </summary>
    /// <value>True to enable push notifications, false to disable all notifications</value>
    /// <example>false</example>
    public bool EnableNotifications { get; set; }
}
