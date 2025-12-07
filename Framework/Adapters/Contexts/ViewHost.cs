using Avalonia.Controls;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Framework.Adapters.Contexts;

public class ViewHost(Control root) : IViewHost
{
    public T RequireContext<T>(string? name = null) where T : class
        => ContextProvider.Require<T>(root, name);

    public T? ResolveContext<T>(string? name = null) where T : class
        => ContextProvider.Resolve<T>(root, name);
}
