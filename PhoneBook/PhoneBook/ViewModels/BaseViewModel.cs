using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PhoneBook.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
        public virtual Task InitializeAsync(object item)
        {
            return Task.FromResult(false);
        }

        public virtual Task InitializeAsync()
        {
            return Task.FromResult(false);
        }
    }
}
