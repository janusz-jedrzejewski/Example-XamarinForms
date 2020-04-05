using Xamarin.Forms.Xaml;

namespace PhoneBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPageXaml, IShellTabPage
    {
        public About()
        {
            InitializeComponent();
        }
    }
}