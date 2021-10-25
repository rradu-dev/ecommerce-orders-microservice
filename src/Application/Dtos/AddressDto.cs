using System;

namespace Ecommerce.Services.Orders.Application.Dtos
{
	public class AddressDto
	{
		public Guid Id { get; set; }
		public string Number { get; set; }
		public string Street { get; set; }
		public String State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
		public string PostalCode { get; set; }
	}
}
