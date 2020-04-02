using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage, IShellTabPage
    {
        public About()
        {
            InitializeComponent();
        }
    }
}