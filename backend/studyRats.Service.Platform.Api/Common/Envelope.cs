using System.Text.Json.Serialization;
using studyRats.Service.Platform.Domain.Abstractions.DomainErrors;

namespace studyRats.Service.Platform.Api.Common
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

        // For Errors that did not come from DTO validation and therefore does not have a Invalid Field
        public static Envelope Error(Error error)
        {
            var errorCode = error?.ErrorCode;
            var errorMessage = error?.Message;
            
            return new Envelope(null, errorCode, errorMessage, null);
        }

        // For erros that did come from the DTO validation and therefore has an invalid field
        public static Envelope Error(Error error, string invalidField)
        {
            var errorCode = error?.ErrorCode;
            var errorMessage = error?.Message;
            return new Envelope(null, errorCode, errorMessage, invalidField);
        }

        // Factory method for Success (you want to use this envelope for successful 200 OK responses)
        public static Envelope Ok(object result)
        {
            if(result == null)
            {
                throw new ArgumentNullException("result in Envelope cannot be null");
            }

            return new Envelope(result, null, null, null);
        }
        public static Envelope Ok()
        {
            return new Envelope(null, null, null, null);
        }
    }
}