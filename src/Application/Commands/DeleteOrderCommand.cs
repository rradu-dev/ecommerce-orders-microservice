using System;
using MediatR;

namespace Ecommerce.Services.Orders.Application.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
