using Avalonia.Controls;
using HelloAvalonia.Framework.Interfaces;

namespace HelloAvalonia.Framework.Models;

public interface INavigationPageStore : IDisposable
{
    Control LoadPageFromNavigation(string path);
}

public abstract class NavigationPageStoreBase(ICompositionScopeFactory scopeFactory)
    : IDisposable, INavigationPageStore
{
    private ICompositionScope? _scope;

    public Control LoadPageFromNavigation(string path)
    {
        _scope?.Dispose();
        _scope = null;
        return CreatePageFromPath(path);
    }

    public void Dispose()
    {
        _scope?.Dispose();
        GC.SuppressFinalize(this);
    }

    protected abstract Control CreatePageFromPath(string path);

    protected Control Resolve<T, TViewModel>()
        where T : Control, new()
    {
        _scope ??= scopeFactory.CreateScope();
        return new T { DataContext = _scope.Resolve<TViewModel>() };
    }
}
