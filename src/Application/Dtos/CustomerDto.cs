using System;

namespace Ecommerce.Services.Orders.Application.Dtos
{
	public class CustomerDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
