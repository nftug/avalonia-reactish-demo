using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Features.AboutPage.Views;
using HelloAvalonia.Features.Counter.Views;

namespace HelloAvalonia.UI.Navigation.Adapters;

public class NavigationPageFactory : INavigationPageFactory
{
    public Control GetPage(Type srcType)
        => throw new NotImplementedException();

    public Control GetPageFromObject(object target) =>
        target switch
        {
            "/counter" => new CounterPage(),
            "/about" => new AboutPage(),
            _ => new TextBlock { Text = "Page not found." },
        };
}
