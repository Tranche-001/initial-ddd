using FluentResults;
using System.Text.RegularExpressions;

namespace studyRats.Service.Platform.Domain.ValueObjects
{
    // Doubts about implementation:
    // Read https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
    // Read https://enterprisecraftsmanship.com/posts/functional-c-primitive-obsession/
    public class Email : ValueObject
    {
        public string Value { get; init; }

        private static readonly int MAX_EMAIL_LENGTH = 100;

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            // 1. Call the validation logic
            var validationResult = isEmailValid(email);

            // 2. Check if it failed. If so, return early with the errors.
            if (validationResult.IsFailed)
            {
                // We convert the Result to Result<Email> to match the return type
                return validationResult.ToResult<Email>();
            }

            // 3. If we got here, everything is valid. Create the object and return success.
            var newEmail = new Email(email);
            return Result.Ok(newEmail);
        }

        private static Result isEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Fail("E-mail can't be empty");

            if (email.Length > MAX_EMAIL_LENGTH)
                return Result.Fail("E-mail is too long");

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                return Result.Fail("E-mail is invalid");
            
            return Result.Ok();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


    }
}



//Beautiful solution with Functional Programming


// public static Result<Email> Create(string email)
//{
//    // We execute validation
//    var validation = isEmailValid(email);

//    // If validation is Ok, ToResult wraps the new Email. 
//    // If validation is Fail, ToResult ignores the new Email and returns the errors.
//    return validation.ToResult(new Email(email));
//}

//private static Result isEmailValid(string email)
//{
//    return Result.Ok()
//        .FailIf(string.IsNullOrWhiteSpace(email), "E-mail can't be empty")
//        .FailIf(email?.Length > MAX_EMAIL_LENGTH, "E-mail is too long")
//        .FailIf(!Regex.IsMatch(email ?? "", @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"), "E-mail is invalid");
//}
