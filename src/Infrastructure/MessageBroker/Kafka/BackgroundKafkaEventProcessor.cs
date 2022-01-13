using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Confluent.Kafka;
using Ecommerce.Services.Orders.Application.MessageBroker;

namespace Ecommerce.Services.Orders.Infrastructure.MessageBroker.Kafka
{
    internal sealed class BackgroundKafkaEventProcessor<TEvent> : BackgroundService
		where TEvent : IEvent
    {
		private readonly ILogger<BackgroundKafkaEventProcessor<TEvent>> _logger;
		private readonly IOptions<KafkaOptions> _options;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public BackgroundKafkaEventProcessor(
			ILogger<BackgroundKafkaEventProcessor<TEvent>> logger,
			IOptions<KafkaOptions> options,
			IServiceScopeFactory serviceScopeFactory)
		{
			_logger = logger;
			_options = options;
			_serviceScopeFactory = serviceScopeFactory;
		}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
			await Task.Yield();
			using (var scope = _serviceScopeFactory.CreateScope())
            {
                var handler = scope.ServiceProvider
					.GetRequiredService<IEventHandler<TEvent>>();

                var builder = new ConsumerBuilder<Guid, TEvent>(_options.Value)
					.SetKeyDeserializer(new KafkaKeyDeserializer())
					.SetValueDeserializer(new KafkaDeserializer<TEvent>());

                using (IConsumer<Guid, TEvent> consumer = builder.Build())
                {
					consumer.Subscribe(_options.Value.Topic);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var result = consumer.Consume(TimeSpan.FromMilliseconds(1000));

                        if (result != null)
                        {
							_logger.LogInformation($"Received message id: {result.Message.Key}");
							await handler.HandleAsync(result.Message.Value);
                            consumer.Commit(result);
                            consumer.StoreOffset(result);
                        }
                    }
                }
			}
        }
    }
}
