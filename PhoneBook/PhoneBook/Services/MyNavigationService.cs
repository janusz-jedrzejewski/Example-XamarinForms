using System.Threading.Tasks;
using Autofac;
using PhoneBook.Enums;
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

        public Task PushAsync<TView, TViewModel>(PageType pageType, object item = null) where TView : ContentPageXaml
            where TViewModel : BaseViewModel
        {
            var view = GetViewPage<TView, TViewModel>(pageType, item);
            return App.Navigation.PushAsync(view);
        }

        public Task PushModalAsync<TView, TViewModel>(PageType pageType, object item = null)
            where TView : ContentPageXaml where TViewModel : BaseViewModel
        {
            var view = GetViewPage<TView, TViewModel>(pageType, item);
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

        private TView GetViewPage<TView, TViewModel>(PageType pageType, object item = null)
            where TView : ContentPageXaml where TViewModel : BaseViewModel
        {
            var view = _container.Resolve<TView>();
            var viewModel = _container.Resolve<TViewModel>();
            view.SetDataContext(viewModel);
            viewModel.InvokeInitialize[pageType](item);

            return view;
        }
    }
}
