using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using Ecommerce.Services.Orders.Core.Entities;
using Ecommerce.Services.Orders.Infrastructure.Database;
using Ecommerce.Services.Orders.Infrastructure.Metrics;
using Ecommerce.Services.Orders.Infrastructure.Tracing;

namespace Ecommerce.Services.Orders.Infrastructure
{
    public static class DependencyInjection
    {
		private static readonly string ElasticSearchKey =
			"Serilog:WriteTo:0:Args:configure:0:Args:nodeUris";

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("orders");
			var elasticSearchNode = configuration[ElasticSearchKey];

            services.AddDatabase(connectionString);
            services.AddRepository<Order, Guid>();

			services.AddServiceDiscovery(builder => builder.UseConsul());
			services.AddMetrics();

			services.AddHealthChecks()
				.AddNpgSql(npgsqlConnectionString: connectionString)
				.AddElasticsearch(elasticSearchNode)
				.AddConsul(options =>
				{
					options.HostName = configuration.GetValue<string>("consul:host");
					options.Port = configuration.GetValue<int>("consul:port");
				});

			services.AddTracing();
            return services;
        }

        public static void UseInfrastructure(
            this IApplicationBuilder app)
        {
            app.Migrate();
			app.UseMetrics();
        }
    }
}
