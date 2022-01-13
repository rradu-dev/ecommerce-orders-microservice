using System;
using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace Ecommerce.Services.Orders.Infrastructure.MessageBroker.Kafka
{
    internal sealed class KafkaDeserializer<TEvent> : IDeserializer<TEvent>
    {
        public TEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (typeof(TEvent) == typeof(Null))
            {
                if (data.Length > 0)
                    throw new ArgumentException("The data is null not null.");
                return default;
            }

            if (typeof(TEvent) == typeof(Ignore))
                return default;

            var dataJson = Encoding.UTF8.GetString(data);

            return JsonSerializer.Deserialize<TEvent>(dataJson);
        }
    }
}
