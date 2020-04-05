using System.Threading.Tasks;
using PhoneBook.Enums;
using PhoneBook.ViewModels;
using PhoneBook.Views;

namespace PhoneBook.Services
{
    public interface IMyNavigationService
    {
        Task PushAsync<TView, TViewModel>(PageType pageType, object item = null) where TView : ContentPageXaml
            where TViewModel : BaseViewModel;

        Task PushModalAsync<TView, TViewModel>(PageType pageType, object item = null) where TView : ContentPageXaml
            where TViewModel : BaseViewModel;

        Task PopAsync();
        Task PopModalAsync();
    }
}
