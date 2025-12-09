using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.UI.Navigation.ViewModels;

namespace HelloAvalonia.Shell.ViewModels;

public class MainWindowViewModel : DisposableBase
{
    public IServiceContainerInstance ContextProvider { get; }

    public NavigationViewModel NavigationViewModel { get; }

    public MainWindowViewModel(IServiceContainerInstance container, NavigationViewModel navigationViewModel)
    {
        ContextProvider = container;
        NavigationViewModel = navigationViewModel;

        NavigationViewModel.InitMenuItems(
            [
                new NavigationViewItem {
                    Content = "Home",
                    Tag = "/",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Home }
                },
                new NavigationViewItem {
                    Content = "Counter List",
                    Tag = "/counter-list",
                    IconSource = new SymbolIconSource { Symbol = Symbol.List }
                }
            ],
            [
                new NavigationViewItem {
                    Content = "About",
                    Tag = "/about",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Help }
                }
            ]
        );
    }
}
