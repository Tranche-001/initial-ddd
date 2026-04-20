using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using studyRats.Service.Platform.Application.Users.Queries;
using studyRats.Service.Platform.Api.Infrastructure;
using studyRats.Service.Platform.Domain.ValueObjects;
using FluentResults;

namespace studyRats.Service.Platform.Api.Controllers.User
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // Sender vem do Mediatr Library
        protected readonly ISender _sender;
        public UserController(
            ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var command = new GetAllUsersQuery();
            var response = await _sender.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            // If response has error of type NotFound, return NotFound with error message
            if (response.IsFailed)
            {
                return NotFound(Envelope.Error(response.Error(), "anything"));
            }
            return Ok(response);
        }
    }
}
