using System;

namespace Ecommerce.Services.Orders.Core.Exceptions
{
    public class NotFoundException : DomainException
    {
        public override string Code { get; } = "not_found";

        private NotFoundException()
            : base("The requested resource was not found.")
        {
        }


		public NotFoundException(Guid id)
			: base($"The resource identified by: '{id}' was not found.")
		{
		}
    }
}
