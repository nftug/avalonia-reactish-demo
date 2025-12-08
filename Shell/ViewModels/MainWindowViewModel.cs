using FluentAvalonia.UI.Controls;
using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Framework.Contexts;
using HelloAvalonia.Framework.ViewModels;
using HelloAvalonia.UI.Adapters;
using HelloAvalonia.UI.Navigation.ViewModels;
using HelloAvalonia.UI.Services;

namespace HelloAvalonia.Shell.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public IDialogService DialogService { get; } = new DialogService();

    public CounterContext CounterContext { get; } = new();

    public NavigationContext NavigationContext { get; } =
        new(
            [
                "/counter",
                "/about",
            ],
            initialPath: "/counter"
        );

    public NavigationViewModel NavigationViewModel { get; } =
        new(
            menuItems: [
                new NavigationViewItem
                {
                    Content = "Counter",
                    IconSource = new SymbolIconSource() { Symbol = Symbol.Add },
                    Tag = "/counter",
                },
            ],
            footerMenuItems: [
                new NavigationViewItem
                {
                    Content = "About",
                    IconSource = new SymbolIconSource() { Symbol = Symbol.Help },
                    Tag = "/about",
                },
            ]
        );
}
