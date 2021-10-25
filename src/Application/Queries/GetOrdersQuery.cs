using MediatR;
using Ecommerce.Services.Orders.Core.Pagination;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Queries
{
    public class GetOrdersQuery : IRequest<PageList<OrderDto>>
    {
        public int Page { get; set; }
		public int Size { get; set; }

        public GetOrdersQuery(int page, int size)
        {
            Page = page;
			Size = size;
        }
    }
}
