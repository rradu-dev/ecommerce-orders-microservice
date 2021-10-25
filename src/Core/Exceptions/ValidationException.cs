using System.Collections.Generic;

namespace Ecommerce.Services.Orders.Core.Exceptions
{
    public class ValidationException : DomainException
    {
        public override string Code { get; } = "valiation";
        public IEnumerable<ValidationResult> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<ValidationResult>();
        }

        public ValidationException(IEnumerable<ValidationResult> failures)
            : this()
        {
            Errors = failures;
        }
    }

    public class ValidationResult
    {
        public string Field { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}
