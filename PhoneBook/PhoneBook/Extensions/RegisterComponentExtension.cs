using Autofac;
using FluentValidation;
using PhoneBook.ViewModels;
using PhoneBook.Views;

namespace PhoneBook.Extensions
{
    public static class RegisterComponentExtension
    {
        public static void RegisterViewViewModel<TView, TViewModel>(this ContainerBuilder builder) where TView : ContentPageXaml
            where TViewModel : BaseViewModel
        {
            builder.RegisterType<TViewModel>().PropertiesAutowired();

            builder
                .RegisterType<TView>()
                .OnActivating(e =>
                {
                    var dep = e.Context.Resolve<TViewModel>();
                    e.Instance.SetDataContext(dep);
                }).As<IShellTabPage>();
        }

        public static void RegisterValidator<TValidator>(this ContainerBuilder builder) where TValidator : IValidator
        {
            builder.RegisterType<TValidator>().AsImplementedInterfaces();
        }

        public static void RegisterViewModel<TViewModel>(this ContainerBuilder builder) where TViewModel : BaseViewModel
        {
            builder.RegisterType<TViewModel>().PropertiesAutowired();
        }
    }
}
