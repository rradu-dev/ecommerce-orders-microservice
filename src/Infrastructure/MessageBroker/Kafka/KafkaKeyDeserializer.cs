using System;
using System.Text;
using Confluent.Kafka;

namespace Ecommerce.Services.Orders.Infrastructure.MessageBroker.Kafka
{
    internal sealed class KafkaKeyDeserializer : IDeserializer<Guid>
    {
        public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
			if (isNull)
			{
				return default;
			}

			var key = Encoding.UTF8.GetString(data);
			if (Guid.TryParse(key, out Guid val))
			{
				return val;
			}

			return default;
        }
    }
}
