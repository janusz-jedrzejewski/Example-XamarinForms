using System.Threading.Tasks;
using PhoneBook.Views;

namespace PhoneBook.Services
{
    public interface IMyNavigationService
    {
        Task PushAsync<TView, TViewModel>(object item) where TView : ContentPageXaml;
        Task PushModalAsync<TView, TViewModel>() where TView : ContentPageXaml;

        Task PopAsync();
        Task PopModalAsync();
    }
}
