using studyRats.Service.Platform.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace studyRats.Service.Platform.Application.Users.Commands.Create
{
    public sealed class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required, Email]
        public string Email { get; set; }
    }
}
