using Microsoft.AspNetCore.Mvc;

namespace studyRats.Service.Platform.Api.Infrastructure
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            // 1. Get the first field that failed validation
            var (fieldName, entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);

            // 2. Grab the string error message we produced in EmailAttribute
            string errorMessage = entry.Errors.First().ErrorMessage;

            // 3. I need to deserialize my custom error message to get the error code and the message
            Error error = Errors.Deserialize(errorMessage);
            Envelope envelope = Envelope.Error

            return new BadRequestObjectResult(response);
        }
    }
}