using PhoneBook.ViewModels;
using Xamarin.Forms;

namespace PhoneBook.Views
{
    public class ContentPageXaml : ContentPage
    {
        public object Context { get; set; }

        public void SetDataContext<T>(T viewModel) where T : BaseViewModel
        {
            BindingContext = viewModel;
            Context = viewModel;

            SetBinding(TitleProperty, new Binding(nameof(BaseViewModel.Title)));
            
            OnPropertyChanged("BindingContext");
        }
    }
}
