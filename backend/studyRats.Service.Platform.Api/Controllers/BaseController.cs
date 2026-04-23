using FluentResults;
using Microsoft.AspNetCore.Mvc;
using studyRats.Service.Platform.Api.Infrastructure;
using studyRats.Service.Platform.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Api.Controllers
{
    public class BaseController: ControllerBase
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult FromResult(ResultBase result)
        {
            if (result.IsSuccess)
            {
                // 1. Try to see if it's a Result<T> (Generic Result)
                // We use reflection/dynamic here to find the 'Value' property 
                // regardless of what 'T' is.
                var valueProperty = result.GetType().GetProperty("Value");
                var value = valueProperty?.GetValue(result);

                // 2. If it has a value, return Ok(value), otherwise just Ok()
                return value is not null ? Ok(value) : Ok();
            }


            if (result.Error() == Errors.General.NotFound())
                return NotFound(Envelope.Error(result.Error()));

            return BadRequest(Envelope.Error(result.Error()));
        }
    }
}
