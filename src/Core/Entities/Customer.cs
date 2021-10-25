using System;

namespace Ecommerce.Services.Orders.Core.Entities
{
	public class Customer : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public Guid OrderId { get; set; }
		public Order Order { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
