using studyRats.Library.Framework.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;


namespace studyRats.Service.Platform.Domain.Entities.Users
{
    public class User 
    {

        public User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        //Implementar Stronged Typed ID's n o futuro;
        public Guid Id { get; private set;}

        public string Name { get; private set;}

        public string Email { get; private set;}

    }
}
