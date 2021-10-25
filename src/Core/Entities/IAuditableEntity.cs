using System;

namespace Ecommerce.Services.Orders.Core.Entities
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
    }
}
