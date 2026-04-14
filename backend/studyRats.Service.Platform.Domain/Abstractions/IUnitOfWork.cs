using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        // CancellationToken allows you to stop the DB operation if the user cancels the request
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
