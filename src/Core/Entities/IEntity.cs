using System;

namespace Ecommerce.Services.Orders.Core.Entities
{
    public interface IEntity<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
    }
}
