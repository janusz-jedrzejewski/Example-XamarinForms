using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using PhoneBook.Common.Resources;
using PhoneBook.Enums;
using PhoneBook.Services;
using Xamarin.Forms;

namespace PhoneBook.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isBusy;
        private string _title = string.Empty;
        public Dictionary<PageType, Action<object>> InvokeInitialize { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public ICommand EditItemCommand { get; }

        public ICommand DeleteItemCommand { get; }

        public ICommand SaveCommand { get; }

        public IValidationService ValidatorService { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        protected BaseViewModel()
        {
            InvokeInitialize = new Dictionary<PageType, Action<object>>()
            {
                {PageType.AddPage, item => InitializeAsync(item)},
                {PageType.DetailPage, item => InitializeDetailAsync(item)},
                {PageType.EditPage, item => InitializeEditAsync(item)}
            };

            SaveCommand = new Command(SaveCommandExecute);
            EditItemCommand = new Command<object>(EditItemCommandExecute);
            DeleteItemCommand = new Command<object>(DeleteItemCommandExecute);
        }

        public virtual Task InitializeEditAsync(object item = null)
        {
            return Task.FromResult(false);
        }

        public virtual Task InitializeDetailAsync(object item = null)
        {
            return Task.FromResult(false);
        }

        public virtual Task InitializeAsync(object item = null)
        {
            return Task.FromResult(false);
        }

        protected virtual async void DeleteItemCommandExecute(object item)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = CaptionResources.DeleteThisItem,
                OkText = CaptionResources.Delete,
                CancelText = CaptionResources.Cancel
            });
            if (result)
            {
                DeleteItemAction(item);
            }
        }

        protected virtual void EditItemCommandExecute(object item)
        {
            EditAction(item);
        }

        protected virtual void SaveCommandExecute(object item)
        {
            var validationResult = ValidatorService?.Validate(item);

            if (validationResult == null || validationResult.IsValid)
            {
                SaveAction(item);
            }
            else
            {
                 UserDialogs.Instance.Alert(validationResult.ToString());
            }
        }

        protected virtual void DeleteItemAction(object item = null)
        {
        }

        protected virtual void SaveAction(object item = null)
        {
        }
        protected virtual void EditAction(object item = null)
        {
        }
    }
}
