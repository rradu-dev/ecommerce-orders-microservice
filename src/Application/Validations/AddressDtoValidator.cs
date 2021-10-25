using FluentValidation;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Validations
{
	public class AddressDtoValidator : AbstractValidator<AddressDto>
	{
		public AddressDtoValidator()
		{
			RuleFor(a => a.Number)
				.NotEmpty()
				.MaximumLength(6);
			RuleFor(a => a.Street)
				.NotEmpty()
				.MaximumLength(100);
			RuleFor(a => a.City)
				.NotEmpty()
				.MaximumLength(24);
			RuleFor(a => a.State)
				.NotEmpty()
				.MaximumLength(24);
			RuleFor(a => a.Country)
				.NotEmpty()
				.MaximumLength(24);
			RuleFor(a => a.PostalCode)
				.NotEmpty()
				.MaximumLength(6)
				.Matches(@"\d{6}");
		}
	}
}
