using FluentResults;
using System;
using System.Text.Json.Serialization;

namespace studyRats.Service.Platform.Api.Infrastructure
{
    public class Envelope
    {
        // Lowercase names for JSON serialization to match your desired output
        [JsonPropertyName("result")]
        public object? Result { get; }

        [JsonPropertyName("errorCode")]
        public string? ErrorCode { get; }

        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; }

        [JsonPropertyName("invalidField")]
        public string? InvalidField { get; }

        [JsonPropertyName("timeGenerated")]
        public DateTime TimeGenerated { get; }

        // Private constructor ensures it can only be created via the static methods
        private Envelope(object? result, string? errorCode, string? errorMessage, string? invalidField)
        {
            Result = result;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            InvalidField = invalidField;
            TimeGenerated = DateTime.UtcNow; // Automatically sets the time
        }

        // Factory method for Errors
        public static Envelope Error(Result result)
        {
            var error = result.Errors.FirstOrDefault();
            var errorCode = error?.Metadata["ErrorCode"]?.ToString();
            var errorMessage = error?.Message;
            var invalidField = error?.Metadata["InvalidField"]?.ToString();
            return new Envelope(null, errorCode, errorMessage, invalidField);
        }

        // Factory method for Success (if you want to use this envelope for successful 200 OK responses too)
        public static Envelope Success(object result)
        {
            return new Envelope(result, null, null, null);
        }
    }
}