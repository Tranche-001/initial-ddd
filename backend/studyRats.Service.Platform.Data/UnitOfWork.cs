using Microsoft.EntityFrameworkCore;
using FluentResults;
using studyRats.Service.Platform.Domain.Abstractions;
using Error = studyRats.Service.Platform.Domain.ValueObjects.Error;
using studyRats.Service.Platform.Domain.ValueObjects;

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
                return Result.Fail(Errors.Database.DbUpdateConcurrency(ex));
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
