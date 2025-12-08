using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Features.AboutPage.Views;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Features.Counter.Views;
using HelloAvalonia.Framework.Adapters.Contexts;

namespace HelloAvalonia.UI.Navigation.Adapters;

public class NavigationPageFactory(IServiceContainerInstance contextProvider) : INavigationPageFactory, IDisposable
{
    private IDisposable? _currentViewModelDisposable;

    public Control GetPage(Type srcType) => throw new NotImplementedException();

    public Control GetPageFromObject(object target)
    {
        _currentViewModelDisposable?.Dispose();
        _currentViewModelDisposable = null;

        return target switch
        {
            "/" => ResolvePage<CounterPage, CounterPageViewModel>(),
            "/about" => new AboutPage(),
            _ => new TextBlock { Text = "Page not found." },
        };
    }

    public void Dispose()
    {
        _currentViewModelDisposable?.Dispose();
        _currentViewModelDisposable = null;
    }

    public TView ResolvePage<TView, TViewModel>()
        where TView : Control, new()
        where TViewModel : notnull
    {
        var ownedViewModel = contextProvider.Resolve<TViewModel>();
        _currentViewModelDisposable = ownedViewModel;

        return new TView() { DataContext = ownedViewModel.Value };
    }
}
