using FluentValidation.Results;

namespace PhoneBook.Services
{
    public interface IValidationService
    {
        ValidationResult Validate<T>(T entity) where T : class;
    }
}
