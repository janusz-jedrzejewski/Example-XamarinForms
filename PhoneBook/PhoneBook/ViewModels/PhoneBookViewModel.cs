using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PhoneBook.Services;
using PhoneBook.Views;
using PhoneBookInterfaces;
using PhoneBookModels;
using Xamarin.Essentials;
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

                UsePhoneAsync(_selectedContact);
                SelectedContact = null;
                RaisePropertyChanged(() => SelectedContact);
            }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public Command LoadItemsCommand { get; }

        public ICommand AddTapCommand { get; }

        public ICommand EditItemCommand { get; }

        public ICommand DeleteItemCommand { get; }


        public PhoneBookViewModel(IPhoneBookService dataService, IMyNavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;

            LoadItemsCommand = new Command(async () => await InitializeAsync());
            AddTapCommand = new Command(AddTabItem);
            EditItemCommand = new Command<Contact>(EditItem);
            DeleteItemCommand = new Command<Contact>(DeleteItem);
        }

        

        public override async Task InitializeAsync()
        {
            IsBusy = true;

            var items = await _dataService.GetIContactAsync(true);

            Contacts = new ObservableCollection<Contact>(items);
            RaisePropertyChanged(() => Contacts);

            IsBusy = false;
        }

        private void UsePhoneAsync(Contact contact)
        {
            var config = new ActionSheetConfig
            {
                Cancel = new ActionSheetOption("Cancel"),
                Title = "Co chcesz zrobić?"
            };

            config.Add("Zadzwonić", new Action(async () =>
            {
                try
                {
                    PhoneDialer.Open(contact.TelephoneNumber);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.Alert("Your device does not support this feature");
                }
                
            }));

            config.Add("SMS", new Action(async () =>
            {
                await Sms.ComposeAsync(new SmsMessage
                {
                    Recipients = new List<string> { contact.TelephoneNumber },
                    Body = ""
                });
            }));
            var dialog = UserDialogs.Instance.ActionSheet(config);
        }

        private async void DeleteItem(Contact contact)
        {
            await _dataService.DeleteContactAsync(contact.Id);
            await InitializeAsync();
        }

        private async void EditItem(Contact contact)
        {
            await _navigationService.PushAsync<UserContactDetailPage, UserContactDetailViewModel>(contact);
        }

        private async void AddTabItem()
        {
            await _navigationService.PushModalAsync<UserContactDetailPage, UserContactDetailViewModel>();
        }
    }
}
