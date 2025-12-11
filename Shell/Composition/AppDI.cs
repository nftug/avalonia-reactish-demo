using HelloAvalonia.Features.Counter.Models;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Features.CounterList.ViewModels;
using HelloAvalonia.Framework.Models;
using HelloAvalonia.Shell.Models;
using HelloAvalonia.Shell.ViewModels;
using HelloAvalonia.UI.Services;
using Pure.DI;

namespace HelloAvalonia.Shell.Composition;

internal static class AppDI
{
    internal static readonly AppContainer Instance = new();

    internal static IConfiguration Setup() =>
        DI.Setup(nameof(AppContainer))
            // Global registrations
            .Bind().To<AppCompositionScopeFactory>()
            .Bind().As(Lifetime.Singleton).To<DialogService>()
            .Bind<NavigationContext>().As(Lifetime.Singleton).To(_ => new NavigationContext("/"))
            .Bind<INavigationPageStore>().As(Lifetime.Singleton).To<AppNavigationPageStore>()
            .RootBind<MainWindowViewModel>("MainWindow").To<MainWindowViewModel>()

            // Counter feature registrations
            .Bind<CounterModel>().As(Lifetime.Singleton).To<CounterModel>()
            .RootBind<CounterPageViewModel>().To<CounterPageViewModel>()

            // CounterList feature registrations
            .Bind<CounterListModel>().As(Lifetime.Scoped).To<CounterListModel>()
            .RootBind<CounterListPageViewModel>().To<CounterListPageViewModel>();
}
