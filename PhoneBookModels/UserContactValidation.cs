using FluentValidation;

namespace PhoneBookModels
{
    public class UserContactValidation : AbstractValidator<Contact>
    {
        public UserContactValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Należy wpisać imię i nazwisko");
            RuleFor(x => x.TelephoneNumber).NotEmpty().WithMessage("Należy wpisać nr telefonu");
        }
    }
}
