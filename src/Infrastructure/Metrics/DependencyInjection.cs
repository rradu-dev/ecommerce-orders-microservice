using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prometheus;
using Prometheus.SystemMetrics;
using Ecommerce.Services.Orders.Infrastructure.Metrics.HostedServices;
using Ecommerce.Services.Orders.Infrastructure.Metrics.Middlewares;

namespace Ecommerce.Services.Orders.Infrastructure.Metrics
{
	internal static class DependencyInjection
	{
		private static readonly string SectionName = "prometheus";

		internal static IServiceCollection AddMetrics(
			this IServiceCollection services)
        {
			var options = GetPrometheusOptions(services);
            if (!options.Enabled)
            {
                return services;
            }

            services.AddHostedService<PrometheusHostedService>();
            services.AddSingleton<PrometheusMiddleware>();
            services.AddSystemMetrics();

            return services;
        }

        internal static IApplicationBuilder UseMetrics(
			this IApplicationBuilder app)
        {
            var options = app.ApplicationServices
				.GetRequiredService<IOptions<PrometheusOptions>>()
				.Value;
            if (!options.Enabled)
            {
                return app;
            }

            var endpoint = string.IsNullOrWhiteSpace(options.Endpoint)
				? "/metrics"
				: options.Endpoint.StartsWith("/")
					? options.Endpoint
					: $"/{options.Endpoint}";

            return app.UseMiddleware<PrometheusMiddleware>()
                .UseHttpMetrics()
                .UseMetricServer(endpoint);
        }

		private static PrometheusOptions GetPrometheusOptions(
			IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

			var section = configuration.GetSection(SectionName);
            services.Configure<PrometheusOptions>(section);

			var options = new PrometheusOptions();
			section.Bind(options);
			return options;
        }
	}
}
