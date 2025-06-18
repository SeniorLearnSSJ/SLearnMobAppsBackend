namespace SeniorLearnApi.DTOs.Responses;

/// <summary>
/// Generic wrapper for all API responses in the SeniorLearn system
/// </summary>
/// <typeparam name="T">The type of data being returned in the response</typeparam>
/// <remarks>
/// Provides a consistent response format across all API endpoints with success status,
/// optional message, and data payload. Used for both successful responses and error responses.
/// </remarks>
/// <example>
/// Success Response:
/// {
///   "success": true,
///   "message": "Operation completed successfully",
///   "data": { ... }
/// }
/// 
/// Error Response:
/// {
///   "success": false,
///   "message": "Validation failed",
///   "data": null
/// }
/// </example>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the API operation was successful
    /// </summary>
    /// <value>True if the operation completed successfully, false if an error occurred</value>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Optional message providing additional information about the response
    /// </summary>
    /// <value>Human-readable message describing the result or error details</value>
    /// <example>Operation completed successfully</example>
    public string? Message { get; set; }

    /// <summary>
    /// The actual data payload returned by the API
    /// </summary>
    /// <value>Strongly-typed data object, null in case of errors</value>
    public T? Data { get; set; }

    private ApiResponse(bool success, string? message, T? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    /// <summary>
    /// Creates a successful API response with data and optional message
    /// </summary>
    /// <param name="data">The data to include in the response</param>
    /// <param name="message">Optional success message</param>
    /// <returns>ApiResponse indicating success with the provided data</returns>
    public static ApiResponse<T> SuccessResponse(T data, string? message = null)
    {
        return new ApiResponse<T>(true, message, data);
    }

    /// <summary>
    /// Creates an error API response with error message and no data
    /// </summary>
    /// <param name="message">Error message describing what went wrong</param>
    /// <returns>ApiResponse indicating failure with the error message</returns>
    public static ApiResponse<T> ErrorResponse(string message)
    {
        return new ApiResponse<T>(false, message, default(T));
    }
}
