using Xamarin.Forms;

namespace PhoneBook.Views
{
    public class ContentPageXaml : ContentPage
    {
        public object Context { get; set; }

        public void SetDataContext<T>(T viewModel)
        {
            BindingContext = viewModel;
            Context = viewModel;

            OnPropertyChanged("BindingContext");
        }
    }
}
