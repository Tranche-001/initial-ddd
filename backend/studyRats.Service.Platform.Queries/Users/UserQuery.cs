using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MediatR;
using studyRats.Service.Platform.Domain.Entities.Users;
using studyRats.Service.Platform.Data;

namespace studyRats.Service.Platform.Queries.Users
{
    public class UserQuery: DbContext
    {
        private readonly DataContext _dataContext;
        public UserQuery(DataContext dataContext )
        {
            _dataContext = dataContext;
        }

        public async Task<User?> GetUserByName(string username)
        {
            var user = await _dataContext.Users
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
