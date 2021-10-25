using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Ecommerce.Services.Orders.Application.Pipelines
{
    internal class PerformancePipeline<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        public PerformancePipeline(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request,
				CancellationToken cancellationToken,
				RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            _logger.LogInformation($"Request: {typeof(TRequest).Name} took {elapsedMilliseconds} ms.");

            return response;
        }
    }
}
