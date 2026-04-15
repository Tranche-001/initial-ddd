using Microsoft.EntityFrameworkCore;
using studyRats.Service.Platform.Data.Repositories;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using studyRats.Service.Platform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;


namespace studyRats.Service.Platform.Data.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {

        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetTopUsers(int count)
        {
            return await _dbSet
                .OrderByDescending(u => u.Name) // Replace with your logic (e.g., Score, CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}
