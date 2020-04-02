using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FluentValidation.Results;
using PhoneBook.Services;
using PhoneBookInterfaces;
using PhoneBookModels;
using Xamarin.Forms;

namespace PhoneBook.ViewModels
{
    public class UserContactDetailViewModel : BaseViewModel
    {
        private readonly IPhoneBookService _dataService;
        private readonly IMyNavigationService _navigationService;
        private bool _isEditMode;
        private Contact _contact;
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value; 
                RaisePropertyChanged(() => Title);
            }
        }

        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value; 
                RaisePropertyChanged(() => Contact);
            }
        }
        
        public ICommand SaveCommand { get;}

        public UserContactDetailViewModel(IPhoneBookService dataService, IMyNavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            SaveCommand = new Command(Save);
        }

        private async void Save()
        {
            var validator = new UserContactValidation();
            ValidationResult result = validator.Validate(_contact);
            if (result.IsValid)
            {
                if (_isEditMode)
                { 
                    await _dataService.UpdateContactAsync(Contact);
                    await _navigationService.PopAsync();
                    return;
                }

                await _dataService.AddContactAsync(Contact);
                await _navigationService.PopModalAsync();
            }
            else
            {
                UserDialogs.Instance.Alert(result.ToString());
            }
            
        }

        public override Task InitializeAsync(object item)
        {
            _isEditMode = true;
            Title = "Edycja";
            Contact = item as Contact;
            RaisePropertyChanged(() => Contact);
            
            return base.InitializeAsync();
        }

        public override Task InitializeAsync()
        {
            _isEditMode = false;
            Title = "Dodaj";
            Contact = new Contact();
            RaisePropertyChanged(() => Contact);

            return base.InitializeAsync();
        }
    }
}
