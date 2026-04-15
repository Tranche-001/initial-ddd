using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using studyRats.Service.Platform.Application.Users.Queries;

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
            return Ok(response);
        }
    }
}
