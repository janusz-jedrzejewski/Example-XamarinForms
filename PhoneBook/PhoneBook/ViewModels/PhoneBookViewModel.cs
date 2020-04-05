using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneBook.Common.Resources;
using PhoneBook.Enums;
using PhoneBook.Services;
using PhoneBook.Views;
using PhoneBookInterfaces;
using PhoneBookModels;
using Xamarin.Forms;

namespace PhoneBook.ViewModels
{
    public class PhoneBookViewModel : BaseViewModel
    {
        private readonly IPhoneBookService _dataService;
        private readonly IMyNavigationService _navigationService;
        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;

                if (_selectedContact == null)
                    return;

                SelectedAsync(_selectedContact);
                SelectedContact = null;
                RaisePropertyChanged(() => SelectedContact);
            }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public ICommand LoadItemsCommand { get; }

        public ICommand AddTapCommand { get; }


        public PhoneBookViewModel(IPhoneBookService dataService, IMyNavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;

            LoadItemsCommand = new Command(async () => await InitializeAsync());
            AddTapCommand = new Command(AddTabItem);
        }

        public override async Task InitializeAsync(object item = null)
        {
            Title = CaptionResources.PhoneBook;
            IsBusy = true;

            var items = await _dataService.GetContactsAsync(true);

            Contacts = new ObservableCollection<Contact>(items);
            RaisePropertyChanged(() => Contacts);

            IsBusy = false;
        }

        protected override async void DeleteItemAction(object contact = null)
        {
            await _dataService.DeleteContactAsync(((Contact)contact).Id);
            await InitializeAsync();
        }

        protected override async void EditAction(object contact = null)
        {
            await _navigationService.PushAsync<UserContactDetailPage, UserContactDetailViewModel>(PageType.EditPage, contact);
        }

        private async void AddTabItem()
        {
            await _navigationService.PushAsync<UserContactDetailPage, UserContactDetailViewModel>(PageType.AddPage);
        }

        private async void SelectedAsync(Contact selectedContact)
        {
            await _navigationService.PushAsync<UserContactDetailPage, UserContactDetailViewModel>(PageType.DetailPage, selectedContact);
        }
    }
}
