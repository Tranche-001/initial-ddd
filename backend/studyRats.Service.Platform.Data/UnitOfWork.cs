using Microsoft.EntityFrameworkCore;
using studyRats.Service.Platform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Data
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;

        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
