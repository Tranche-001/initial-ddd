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
using System.Net.Sockets;
using Microsoft.AspNetCore.Components;
using studyRats.Service.Platform.Application.Users.Commands.Create;

namespace studyRats.Service.Platform.Api.Controllers.User
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : BaseController
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
            var result = await _sender.Send(command);
            return FromResult(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var command = new CreateUserCommand(dto.Name, dto.Email);
            var result = await _sender.Send(command);
            return FromResult(result);
        }
    }
}
