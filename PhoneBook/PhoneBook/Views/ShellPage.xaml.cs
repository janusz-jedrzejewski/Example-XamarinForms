using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        public ShellPage(IEnumerable<IShellTabPage> tabs)
        {
            AddTabPage(tabs);
            InitializeComponent();
        }

        private void AddTabPage(IEnumerable<IShellTabPage> tabs)
        {
            var tabBar = new TabBar();
            foreach (var tabPage in tabs)
            {
                var tab = new Tab();
                tab.Title = (tabPage as ContentPage)?.Title;
                var shellContent = new ShellContent();
                var dataTemplate = new DataTemplate(() => tabPage);
                shellContent.ContentTemplate = dataTemplate;
                tab.Items.Add(shellContent);

                tabBar.Items.Add(tab);
            }
            this.Items.Add(tabBar);
        }
    }
}