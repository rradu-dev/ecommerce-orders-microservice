using FluentValidation;
using Ecommerce.Services.Orders.Application.Commands;

namespace Ecommerce.Services.Orders.Application.Validations
{
    public class CreateOrderCommandValidator
		: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.Status)
				.IsInEnum();
			RuleFor(c => c.Customer)
				.SetValidator(new CustomerDtoValidator());
			RuleFor(c => c.ShippingAddress)
				.SetValidator(new AddressDtoValidator());
            RuleFor(c => c.Items)
                .NotEmpty();
            RuleForEach(c => c.Items)
				.SetValidator(new OrderItemDtoValidator());
        }
    }
}
