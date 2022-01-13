using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Services.Orders.Application.Events;
using Ecommerce.Services.Orders.Application.Handlers;
using Ecommerce.Services.Orders.Application.MessageBroker;
using Ecommerce.Services.Orders.Infrastructure.MessageBroker.Kafka;

namespace Ecommerce.Services.Orders.Infrastructure.MessageBroker
{
    public static class DependencyInjection
    {
		private static readonly string SectionName = "kafka";

		public static IServiceCollection AddKafka(
			this IServiceCollection services)
		{
			var options = GetKafkaConsumerOptions(services);
			if (!options.Enabled)
			{
				return services;
			}

			/* services.AddScoped<IEventHandler<ProductUpdatedEvent>, ProductUpdatedEventHandler>();
			services.AddHostedService<BackgroundKafkaEventProcessor<ProductUpdatedEvent>>(); */

			return services;
		}

		private static KafkaOptions GetKafkaConsumerOptions(
			IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

			var section = configuration.GetSection(SectionName);
            services.Configure<KafkaOptions>(section);

			var options = new KafkaOptions();
			section.Bind(options);
			return options;
        }
	}
}
