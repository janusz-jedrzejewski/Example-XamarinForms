using System.Collections.Generic;
using PhoneBook.ViewModels;
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
            foreach (ContentPageXaml tabPage in tabs)
            {
                var shellContent = new ShellContent();
                var dataTemplate = new DataTemplate(() => tabPage);
                shellContent.ContentTemplate = dataTemplate;

                var tab = CreateTabWithContext(tabPage);
                tab.Items.Add(shellContent);

                tabBar.Items.Add(tab);
            }
            this.Items.Add(tabBar);
        }

        private Tab CreateTabWithContext(ContentPageXaml tabPage)
        {
            var tab = new Tab();
            var sourceBindingContext = tabPage.BindingContext as BaseViewModel;

            tab.SetBinding(Tab.TitleProperty, new Binding(nameof(BaseViewModel.Title), mode: BindingMode.TwoWay, source: sourceBindingContext));

            return tab;
        }
    }
}