using FluentValidation;
using Ecommerce.Services.Orders.Application.Dtos;

namespace Ecommerce.Services.Orders.Application.Validations
{
	public class CustomerDtoValidator : AbstractValidator<CustomerDto>
	{
		public CustomerDtoValidator()
		{
			RuleFor(c => c.FirstName)
				.NotEmpty()
				.MaximumLength(24);
			RuleFor(c => c.LastName)
				.NotEmpty()
				.MaximumLength(24);
			RuleFor(c => c.Email)
				.NotEmpty()
				.MaximumLength(64)
				.EmailAddress();
			RuleFor(c => c.PhoneNumber)
				.NotEmpty()
				.MaximumLength(12)
				.Matches(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");
		}
	}
}
