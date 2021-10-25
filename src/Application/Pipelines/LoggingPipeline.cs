using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Ecommerce.Services.Orders.Application.Pipelines
{
    internal class LoggingPipeline<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingPipeline(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
			CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
        {
			var requestName = typeof(TRequest).Name;
			_logger.LogInformation("Handling request: {@Request}", request);
            var response = await next();
			_logger.LogInformation("Handled request: {requestName}. Response: {@Response}", requestName, response);

            return response;
        }
    }
}
