using Application.Common.Contracts;
using Core.Application.Common.Contracts;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Core.Application.Common.Behaviors
{
    internal class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private ILogger<TRequest> _logger;
        private ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? "Anonymous";

            _logger.LogInformation($"Processing Request: {requestName} " +
                                   $"| UserId: {userId} " +
                                   $"| Data: {request}");

            return Task.CompletedTask;
        }
    }
}
