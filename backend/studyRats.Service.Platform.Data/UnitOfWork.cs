using Microsoft.EntityFrameworkCore;
using studyRats.Service.Platform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentResults;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = FluentResults.Error;

namespace studyRats.Service.Platform.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;

        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return Result.Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Another user edited this record at the exact same time.
                return Result.Fail(new Error("Data was modified by another user. Please reload.").CausedBy(ex));
            }
            catch (DbUpdateException ex)
            {
                // A database rule was broken (e.g., duplicate email address).
                return Result.Fail(new Error("A database constraint was violated.").CausedBy(ex));
            }
            // We intentionally DO NOT catch SqlException, TimeoutException, etc.
            // Those are true exceptions and should be handled by the global exception handler,
            // which will log them and return a 500 Internal Server Error.
            // This is because they indicate a problem with the database connection or configuration.
            // To understand more what qualifies as true exceptions
            // https://enterprisecraftsmanship.com/posts/what-is-exceptional-situation/
            // https://enterprisecraftsmanship.com/posts/error-handling-exception-or-result/
            // https://enterprisecraftsmanship.com/posts/exceptions-for-flow-control/
            // If the database is offline, let the system throw a 500 Internal Server Error.
        }
    }
}
