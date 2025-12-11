using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.UI.Navigation.ViewModels;

namespace HelloAvalonia.Shell.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public NavigationViewModel NavigationViewModel { get; }

    public MainWindowViewModel(NavigationViewModel navigationViewModel)
    {
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
