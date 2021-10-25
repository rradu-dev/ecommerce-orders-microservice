using System;

namespace Ecommerce.Services.Orders.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public virtual string Code { get; }

        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }
    }
}
