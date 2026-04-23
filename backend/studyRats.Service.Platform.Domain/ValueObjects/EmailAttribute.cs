using FluentResults;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace studyRats.Service.Platform.Domain.ValueObjects;

[AttributeUsage(AttributeTargets.Property)]
public sealed class EmailAttribute : ValidationAttribute
{

    // Doubts about implementation
    // Read https://enterprisecraftsmanship.com/posts/combining-asp-net-core-attributes-with-value-objects/
    // Tried to make mixture of that with FluentResults. 
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // 1. Handle nulls
        // Usually, we let the [Required] attribute handle null checks.
        if (value == null)
        {
            return ValidationResult.Success;
        }

        // 2. Ensure the value is a string
        if (value is not string email)
        {
            return new ValidationResult(Errors.General.ValueTypeIsInvalid("Email").Serialize());
        }

        // 3. Call your Domain Value Object logic
        Result<Email> emailResult = Email.Create(email);

        // 4. Handle the Result
        if (emailResult.IsFailed)
        {
            // * Extract the error message from the Result.email 
            var errorMessage = emailResult.Error().Serialize();

            return new ValidationResult(errorMessage);
        }

        // 5. If everything passed, return success
        return ValidationResult.Success;
    }
}