using System;
using System.Collections.Generic;
using System.Text;
using studyRats.Service.Platform.Domain.Entities;

namespace studyRats.Service.Platform.Domain.Entities.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        void Add(User user);

        void Delete(User user);


    }
}
