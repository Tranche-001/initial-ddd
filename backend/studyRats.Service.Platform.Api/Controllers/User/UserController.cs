using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using studyRats.Service.Platform.Application.Users.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Api.Controllers.User
{
    [Route("api/users/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        protected readonly IMediator _mediator;
        public UserController(
            IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateNewUser(CreateUserRequest request)
        {
            var command = new CreateUserCommand();
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
