using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PhoneBook.Common.Resources;
using PhoneBook.Services;
using PhoneBookInterfaces;
using PhoneBookModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhoneBook.ViewModels
{
    public class UserContactDetailViewModel : BaseViewModel
    {
        private readonly IPhoneBookService _dataService;
        private readonly IMyNavigationService _navigationService;
        private bool _isEditMode;
        private Contact _contact;
        private bool _isButtonSaveVisible;
        private bool _isPhoneDialerSmsButtonsVisible;
        private bool _isEditingButtonsTabVisible = true;

        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value; 
                RaisePropertyChanged(() => Contact);
            }
        }

        public bool IsButtonSaveVisible
        {
            get => _isButtonSaveVisible;
            set
            {
                _isButtonSaveVisible = value;
                if (value)
                {
                    IsPhoneDialerSmsButtonsVisible = false;
                    IsEditingButtonsTabVisible = false;
                }
                RaisePropertyChanged(() => IsButtonSaveVisible);
            }
        }

        public bool IsPhoneDialerSmsButtonsVisible
        {
            get => _isPhoneDialerSmsButtonsVisible;
            set
            {
                if (value)
                {
                    IsButtonSaveVisible = false;
                    IsEditingButtonsTabVisible = true;
                }
                _isPhoneDialerSmsButtonsVisible = value; 
                RaisePropertyChanged(() => IsPhoneDialerSmsButtonsVisible);
            }
        }

        public bool IsEditingButtonsTabVisible
        {
            get => _isEditingButtonsTabVisible;
            set
            {
                _isEditingButtonsTabVisible = value; 
                RaisePropertyChanged(() => IsEditingButtonsTabVisible);
            }
        }

        public ICommand CallCommand { get; }
        public ICommand SendSmsCommand { get; }


        public UserContactDetailViewModel(IPhoneBookService dataService, IMyNavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;

            CallCommand = new Command(CallExecute);
            SendSmsCommand = new Command(SendSmsExecute);
        }

        public override Task InitializeEditAsync(object item = null)
        {
            _isEditMode = true;
            IsButtonSaveVisible = true;
            Title = CaptionResources.Edition;
            Contact = item as Contact;

            return base.InitializeAsync();
        }

        public override Task InitializeAsync(object item = null)
        {
            _isEditMode = false;
            IsButtonSaveVisible = true;
            Title = CaptionResources.Add;
            Contact = new Contact();

            return base.InitializeAsync();
        }

        public override Task InitializeDetailAsync(object item = null)
        {
            _isEditMode = false;
            IsPhoneDialerSmsButtonsVisible = true;
            Title = CaptionResources.Details;
            Contact = item as Contact;

            return base.InitializeAsync();
        }

        protected override async void SaveAction(object item = null)
        {
            if (_isEditMode)
            {
                await _dataService.UpdateContactAsync(Contact);
                await _navigationService.PopAsync();
                return;
            }

            await _dataService.AddContactAsync(Contact);
            await _navigationService.PopAsync();
        }

        protected override async void DeleteItemAction(object item = null)
        {
            await _dataService.DeleteContactAsync(((Contact)item).Id);
            await _navigationService.PopAsync();
        }

        protected override async void EditAction(object item = null)
        {
            await InitializeEditAsync(item);
        }


        private void SendSmsExecute()
        {
            try
            {
                Sms.ComposeAsync(new SmsMessage
                {
                    Recipients = new List<string> { _contact.TelephoneNumber },
                    Body = ""
                });
            }
            catch (Exception)
            {
                UserDialogs.Instance.Alert(CaptionResources.YourDeviceDoesNotSupportThisFeature);
            }
        }

        private void CallExecute()
        {
            try
            {
                PhoneDialer.Open(_contact.TelephoneNumber);
            }
            catch (Exception)
            {
                UserDialogs.Instance.Alert(CaptionResources.YourDeviceDoesNotSupportThisFeature);
            }
        }
    }
}
