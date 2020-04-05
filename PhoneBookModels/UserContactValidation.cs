using FluentValidation;
using PhoneBook.Common.Resources;

namespace PhoneBookModels
{
    public class UserContactValidation : AbstractValidator<Contact>
    {
        public UserContactValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(CaptionResources.EnterNmeAndLastName);
            RuleFor(x => x.TelephoneNumber).NotEmpty().WithMessage(CaptionResources.EnterTelefoneNumber);
        }
    }
}
