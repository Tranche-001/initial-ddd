using System;
using System.Collections.Generic;
using System.Text;
using studyRats.Service.Platform.Domain.Entities.Users;

namespace studyRats.Service.Platform.Domain.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetTopUsers(int count);

    }
}
