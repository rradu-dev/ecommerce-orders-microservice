using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Ecommerce.Services.Orders.Core.Exceptions;
using ValidationException = Ecommerce.Services.Orders.Core.Exceptions.ValidationException;

namespace Ecommerce.Services.Orders.Application.Pipelines
{
    internal class ValidationPipeline<TRequest, TResponse>
		: IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request,
			CancellationToken cancellationToken,
			RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task
					.WhenAll(_validators.Select(v =>
						v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .GroupBy(f => f.PropertyName, e => e.ErrorMessage)
                    .Select(f => new ValidationResult
					{
						Field = f.Key,
						Messages = f.ToList()
					})
                    .ToList();

                if (failures.Count() != 0)
                {
					System.Console.WriteLine(failures.Count());
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
