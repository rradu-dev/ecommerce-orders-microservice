using FluentValidation;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Validations
{
	public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
	{
		public OrderItemDtoValidator()
		{
			RuleFor(i => i.ProductId)
				.NotEmpty();
            RuleFor(i => i.Title)
                .NotEmpty()
				.MaximumLength(50);
            RuleFor(i => i.Description)
                .NotEmpty()
				.MaximumLength(255);
            RuleFor(i => i.Thumbnail)
                .NotEmpty()
				.MaximumLength(128);
            RuleFor(i => i.Quantity)
				.GreaterThan(0);
			RuleFor(i => i.UnitPrice)
				.GreaterThan(0);
		}
	}
}
