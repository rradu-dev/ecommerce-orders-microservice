using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenTracing;
using OpenTracing.Tag;

namespace Ecommerce.Services.Orders.Infrastructure.Tracing.Pipelines
{
    internal class JaegerPipeline<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ITracer _tracer;

        public JaegerPipeline(ITracer tracer)
        {
			_tracer = tracer;
        }

        public async Task<TResponse> Handle(TRequest request,
			CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
        {
			var requestName = ToUnderscoreCase(typeof(TRequest).Name);
            using var scope = BuildScope(requestName);
            var span = scope.Span;

            try
            {
                span.Log($"Handling request: {requestName}");
                var response = await next();
                span.Log($"Handled request: {requestName}");
				return response;
            }
            catch (Exception ex)
            {
                span.Log(ex.Message);
                span.SetTag(Tags.Error, true);
                throw;
            }
        }

		private IScope BuildScope(string request)
        {
            var scope = _tracer
                .BuildSpan($"handling-{request}")
                .WithTag("request-type", request);

            if (_tracer.ActiveSpan is {})
            {
                scope.AddReference(References.ChildOf, _tracer.ActiveSpan.Context);
            }

            return scope.StartActive(true);
        }

		private static string ToUnderscoreCase(string str)
            => string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x)
					? "-" + x
					: x.ToString())
				)
				.ToLowerInvariant();
    }
}
