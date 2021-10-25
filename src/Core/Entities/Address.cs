using System;

namespace Ecommerce.Services.Orders.Core.Entities
{
	public class Address : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public Guid OrderId { get; set; }
		public Order Order { get; set; }
		public string Number { get; set; }
		public string Street { get; set; }
		public String State { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string PostalCode { get; set; }
	}
}

