using FluentResults;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace studyRats.Service.Platform.Domain.Abstractions.DomainErrors
{
    // Inheriting from ValueObject gives us equality for free!
    public class Error : ValueObject, IError
    {
        public string Message { get; }
        public string ErrorCode { get; }
        public Dictionary<string, object> Metadata { get; } = new();
        public List<IError> Reasons { get; } = new();

        protected Error()
        {
            Metadata = new Dictionary<string, object>();
            Reasons = new List<IError>();
        }

        public Error(string message) : this()
        {
            Message = message;
            ErrorCode = "unspecified.code";
        }

        public Error(string message, string errorCode) : this()
        {
            Message = message;
            ErrorCode = errorCode;
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
                // We are expecting that the serialized data is in the correct format, but if it's not, we can return a generic error
                return new Error(serializedData, "validation.error");
            }
        }

        //More code from FluentResults' Error class that we might want;
        public Error CausedBy(IError error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            Reasons.Add(error);
            return this;
        }

        public Error CausedBy(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Reasons.Add(new ExceptionalError(exception));
            return this;
        }
    }
}