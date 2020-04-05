using PhoneBook.ViewModels;
using Xamarin.Forms.Xaml;

namespace PhoneBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneBookPage : ContentPageXaml, IShellTabPage
    {
        public PhoneBookPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is BaseViewModel context)
            {
                await context.InitializeAsync();
            }
        }
    }
}