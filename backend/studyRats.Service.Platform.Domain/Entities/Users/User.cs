using studyRats.Library.Framework.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Domain.Entities.User
{
    public class User : Entity<User>
    {
        //Implementar Stronged Typed ID's no futuro;
        public Guid Id { get; set;}

        public string Name { get; set;}

        public string Email { get; set;}

    }
}
