// Infrastructure/Middleware/GlobalExceptionHandler.cs
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using studyRats.Service.Platform.Api.Common;
using studyRats.Service.Platform.Domain.Abstractions.DomainErrors;
using System.Net;
using System.Text.Json;

namespace studyRats.Service.Platform.Api.Infrastructure.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Exceções que chegam aqui são bugs ou falhas catastróficas.
            // A responsabilidade é: logar tudo, não vazar nada.
            _logger.LogError(
                exception,
                "Unhandled exception. TraceId: {TraceId} | Path: {Path} | Method: {Method}",
                httpContext.TraceIdentifier,
                httpContext.Request.Path,
                httpContext.Request.Method);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var envelope = Envelope.Error(
                Errors.General.InternalServerError()
            );

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(envelope),
                cancellationToken);

            return true; // true = exceção foi tratada, pipeline para aqui
        }
    }
}