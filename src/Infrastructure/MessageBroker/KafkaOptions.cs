using Confluent.Kafka;

namespace Ecommerce.Services.Orders.Infrastructure.MessageBroker
{
	public class KafkaOptions : ClientConfig
	{
		public bool Enabled { get; set; }
		public string Topic { get; set; }
	}
}
