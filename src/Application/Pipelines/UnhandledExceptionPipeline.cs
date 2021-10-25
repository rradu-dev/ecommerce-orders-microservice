using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Ecommerce.Services.Orders.Application.Pipelines
{
    internal class UnhandledExceptionPipeline<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionPipeline(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
			CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception for request {@Request}", request);
                throw;
            }
        }
    }
}
