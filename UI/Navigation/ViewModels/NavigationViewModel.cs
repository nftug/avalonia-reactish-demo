using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Contexts;
using HelloAvalonia.UI.Navigation.Adapters;
using R3;

namespace HelloAvalonia.UI.Navigation.ViewModels;

public class NavigationViewModel : DisposableBase
{
    private readonly NavigationContext _context;
    private readonly ReactiveCommand<string> _navigateCommand;

    public IReadOnlyBindableReactiveProperty<string> PageTitle { get; }
    public Observable<string> NavigateRequested => _navigateCommand;
    public BindableReactiveProperty<NavigationViewItem?> SelectedItem { get; }

    public IEnumerable<NavigationViewItem> MenuItems { get; private set; } = [];
    public IEnumerable<NavigationViewItem> FooterMenuItems { get; private set; } = [];

    public NavigationPageFactory PageFactory { get; }

    public NavigationViewModel(NavigationContext context, NavigationPageFactory pageFactory)
    {
        _context = context;

        _navigateCommand = new ReactiveCommand<string>().AddTo(Disposable);
        SelectedItem = new BindableReactiveProperty<NavigationViewItem?>().AddTo(Disposable);

        PageFactory = pageFactory;

        PageTitle = _context.CurrentPath
            .Select(path =>
            {
                var menuItem = FindMenuItemByPath(path);
                return menuItem?.Content?.ToString() ?? string.Empty;
            })
            .ToReadOnlyBindableReactiveProperty(string.Empty)
            .AddTo(Disposable);

        _context.CurrentPath
            .ObserveOnUIThreadDispatcher()
            .Subscribe(_navigateCommand.Execute)
            .AddTo(Disposable);

        _context.CurrentPath
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
                    if (!await _context.NavigateAsync(path, ct))
                    {
                        // Revert selection if navigation failed
                        SelectedItem.Value = FindMenuItemByPath(_context.CurrentPath.CurrentValue);
                    }
                }
            })
            .AddTo(Disposable);
    }

    public void InitMenuItems(
        IEnumerable<NavigationViewItem> menuItems,
        IEnumerable<NavigationViewItem> footerMenuItems)
    {
        MenuItems = menuItems;
        FooterMenuItems = footerMenuItems;

        SelectedItem.Value = FindMenuItemByPath(_context.CurrentPath.CurrentValue);
    }

    private NavigationViewItem? FindMenuItemByPath(string path)
        => MenuItems.Concat(FooterMenuItems)
            .FirstOrDefault(item => item.Tag is string itemPath && itemPath == path);
}
