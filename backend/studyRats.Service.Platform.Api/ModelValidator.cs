using Microsoft.AspNetCore.Mvc;
using studyRats.Service.Platform.Api.Common;
using studyRats.Service.Platform.Domain.Abstractions.DomainErrors;

namespace studyRats.Service.Platform.Api.Infrastructure
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            // 1. Get the first field that failed validation
            var (fieldName, entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);

            // 2. Grab the string error message we produced from the fieldAttribrute.
            string errorMessage = entry.Errors.First().ErrorMessage;

            // 3. I need to deserialize my custom error message to get the error code and the message
            Error error = Error.Deserialize(errorMessage);
            Envelope envelope = Envelope.Error(error, fieldName);

            return new BadRequestObjectResult(envelope);
        }
    }
}