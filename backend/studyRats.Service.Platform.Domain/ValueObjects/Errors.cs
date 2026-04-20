using FluentResults;


namespace studyRats.Service.Platform.Domain.ValueObjects
{
    public class Errors
    {
        public static class General
        {
            public static Error NotFound(string entityName, string identifier)
            {
                var error = new Error($"{entityName} with identifier {identifier} was not found.", "not.found");
                return error;
            }

            public static Error ValueTypeIsInvalid(string fieldName)
            {
                var error = new Error($"The value for field '{fieldName}' is of an invalid type.", "value.type.is.invalid");
                return error;
            }
        }

        public static class Email
        {
            public static Error Empty()
            {
                var error = new Error("E-mail can't be empty.", "email.cant.be.empty");
                return error;
            }
            public static Error TooLong()
            {
                var error = new Error("E-mail is too long.", "email.too.long");
                return error;
            }
            public static Error InvalidFormat()
            {
                var error = new Error("E-mail is invalid.", "email.invalid.format");
                return error;
            }
        }
    }
}
