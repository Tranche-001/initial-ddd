using FluentResults;

namespace studyRats.Service.Platform.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        // CancellationToken allows you to stop the DB operation if the user cancels the request
        Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
