using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services.Orders.Infrastructure.Metrics.Middlewares
{
    internal sealed class PrometheusMiddleware : IMiddleware
    {
        private readonly ISet<string> _allowedHosts;
        private readonly string _endpoint;
        private readonly string _apiKey;

        public PrometheusMiddleware(IOptions<PrometheusOptions> options)
        {
            _allowedHosts = new HashSet<string>(
					options.Value.AllowedHosts ?? Array.Empty<string>());
            _endpoint = string.IsNullOrWhiteSpace(options.Value.Endpoint)
				? "/metrics"
				: options.Value.Endpoint.StartsWith("/")
					? options.Value.Endpoint
					: $"/{options.Value.Endpoint}";
            _apiKey = options.Value.ApiKey;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var request = context.Request;
            if (request.Path != _endpoint)
            {
                return next(context);
            }

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                return next(context);
            }

            if (request.Query.TryGetValue("apiKey", out var apiKey)
				&& apiKey == _apiKey)
            {
                return next(context);
            }

            var host = context.Request.Host.Host;
            if (_allowedHosts.Contains(host))
            {
                return next(context);
            }

            if (!request.Headers.TryGetValue("x-forwarded-for", out var forwardedFor))
            {
                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            }

            if (_allowedHosts.Contains(forwardedFor))
            {
                return next(context);
            }

            context.Response.StatusCode = 404;
            return Task.CompletedTask;
        }
    }
}
