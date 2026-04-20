using FluentResults;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace studyRats.Service.Platform.Domain.ValueObjects
{
    // Inheriting from ValueObject gives us equality for free!
    public class Error : ValueObject, IError
    {
        public string Message { get; }
        public string ErrorCode { get; }
        public Dictionary<string, object> Metadata { get; } = new();
        public List<IError> Reasons { get; } = new();

        public Error(string message, string errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
            Metadata.Add("ErrorCode", errorCode);
        }

        // ValueObject Equality: We define that ErrorCode is what makes an error unique
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ErrorCode;
        }

        // =====================================================================
        // SERIALIZATION LOGIC
        // =====================================================================

        public string Serialize()
        {
            return JsonSerializer.Serialize(new { Message, ErrorCode });
        }

        public static Error Deserialize(string serializedData)
        {
            try
            {
                var data = JsonSerializer.Deserialize<JsonElement>(serializedData);
                return new Error(
                    data.GetProperty("Message").GetString()!,
                    data.GetProperty("ErrorCode").GetString()!
                );
            }
            catch
            {
                // Fallback for non-JSON strings (standard ASP.NET errors)
                return new Error(serializedData, "validation.error");
            }
        }

        // Necessary to satisfy the IError interface for FluentResults
        public override string ToString() => $"[{ErrorCode}] {Message}";
    }
}