using FluentValidation;
using FluentValidation.Results;

namespace PhoneBook.Services
{
    public class FluentValidationService : IValidationService
    {
        private readonly IValidatorFactory _validatorFactory;

        public FluentValidationService(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public ValidationResult Validate<T>(T item) where T : class
        {
            var validator = _validatorFactory.GetValidator(item.GetType());
            var result = validator.Validate(item);
            return result;
        }
    }
}
