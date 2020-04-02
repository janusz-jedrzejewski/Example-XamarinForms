using System.Threading.Tasks;
using Autofac;
using PhoneBook.ViewModels;
using PhoneBook.Views;
using Xamarin.Forms;

namespace PhoneBook.Services
{
    public class MyNavigationService : IMyNavigationService
    {
        private readonly IComponentContext _container;

        public MyNavigationService(IComponentContext container)
        {
            _container = container;
        }

        public Task PushAsync<TView, TViewModel>(object item) where TView : ContentPageXaml
        {
            var view = _container.Resolve<TView>();
            var viewModel = _container.Resolve<TViewModel>();
            view.SetDataContext(viewModel);
            (viewModel as BaseViewModel)?.InitializeAsync(item);
            return App.Navigation.PushAsync(view);
        }

        public Task PushModalAsync<TView, TViewModel>() where TView : ContentPageXaml
        {
            var view = _container.Resolve<TView>();
            var viewModel = _container.Resolve<TViewModel>();
            view.SetDataContext(viewModel);
            (viewModel as BaseViewModel)?.InitializeAsync();
            return App.Navigation.PushModalAsync(new NavigationPage(view));
        }

        public Task PopAsync()
        {
            return App.Navigation.PopAsync();
        }

        public Task PopModalAsync()
        {
            return App.Navigation.PopModalAsync();
        }
    }
}
