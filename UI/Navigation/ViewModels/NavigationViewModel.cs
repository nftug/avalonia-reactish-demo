using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.Contexts;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.UI.Navigation.ViewModels;

public partial class NavigationViewModel : ViewModelBase
{
    private readonly ReactiveCommand<string> _navigateCommand;

    [ObservableProperty] IReadOnlyBindableReactiveProperty<string>? pageTitle;
    public Observable<string> NavigateRequested => _navigateCommand;
    public BindableReactiveProperty<NavigationViewItem?> SelectedItem { get; }

    public IEnumerable<NavigationViewItem> MenuItems { get; }
    public IEnumerable<NavigationViewItem> FooterMenuItems { get; }

    public NavigationViewModel(
        IEnumerable<NavigationViewItem> menuItems, IEnumerable<NavigationViewItem> footerMenuItems)
    {
        MenuItems = menuItems;
        FooterMenuItems = footerMenuItems;
        _navigateCommand = new ReactiveCommand<string>().AddTo(Disposable);
        SelectedItem = new BindableReactiveProperty<NavigationViewItem?>().AddTo(Disposable);
    }

    public override void AttachViewHost(IViewHost viewHost)
    {
        var context = viewHost.RequireContext<NavigationContext>();

        PageTitle = context.CurrentPath
            .Select(path =>
            {
                var menuItem = FindMenuItemByPath(path);
                return menuItem?.Content?.ToString() ?? string.Empty;
            })
            .ToReadOnlyBindableReactiveProperty(string.Empty)
            .AddTo(Disposable);

        context.CurrentPath
            .ObserveOnUIThreadDispatcher()
            .Subscribe(_navigateCommand.Execute)
            .AddTo(Disposable);

        context.CurrentPath
            .Select(FindMenuItemByPath)
            .WhereNotNull()
            .Subscribe(item => SelectedItem.Value = item)
            .AddTo(Disposable);

        SelectedItem
            .WhereNotNull()
            .SubscribeAwait(async (item, ct) =>
            {
                if (item.Tag is string path)
                {
                    bool navigated = await context.NavigateAsync(path, ct);
                    if (!navigated)
                    {
                        // Revert selection if navigation failed
                        var currentPath = context.CurrentPath.CurrentValue;
                        SelectedItem.Value = FindMenuItemByPath(currentPath);
                    }
                }
            })
            .AddTo(Disposable);
    }

    private NavigationViewItem? FindMenuItemByPath(string path)
        => MenuItems.Concat(FooterMenuItems)
            .FirstOrDefault(item => item.Tag is string itemPath && itemPath == path);
}
