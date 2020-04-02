using Autofac;
using PhoneBook.Services;
using PhoneBook.ViewModels;
using PhoneBook.Views;
using PhoneBookDataService;
using PhoneBookInterfaces;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PhoneBook
{
    public partial class App : Application
    {
        private IContainer _containerBuilder;
        public static INavigation Navigation;

        public App()
        {
            InitializeComponent();

            DependencyResolver.ResolveUsing(type => _containerBuilder.IsRegistered(type) ? _containerBuilder.Resolve(type) : null);

            RegisterContainer();

            var shellPage = _containerBuilder.Resolve<ShellPage>();

            MainPage = shellPage;

            Navigation = MainPage.Navigation;
            ;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ShellPage>().SingleInstance();
            builder.RegisterType<PhoneBookViewModel>();

            builder
                .RegisterType<PhoneBookPage>()
                .OnActivating(e =>
                {
                    var dep = e.Context.Resolve<PhoneBookViewModel>();
                    e.Instance.SetDataContext(dep);
                }).As<IShellTabPage>();
            builder.RegisterType<About>().As<IShellTabPage>();


            builder.RegisterType<UserContactDetailViewModel>();
            builder.RegisterType<UserContactDetailPage>();

            builder.RegisterType<MyNavigationService>().As<IMyNavigationService>().SingleInstance();
            builder.RegisterType<MockPhoneBookService>().As<IPhoneBookService>().SingleInstance();


            _containerBuilder = builder.Build();
        }
    }
}
