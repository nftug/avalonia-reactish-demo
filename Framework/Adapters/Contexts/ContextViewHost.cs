using Avalonia.Controls;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Framework.Adapters.Contexts;

public class ContextViewHost(Control root) : IContextViewHost
{
    public ContextReturn<T> RequireContext<T>(string? name = null) where T : class
        => ContextProvider.Require<T>(root, name);

    public ContextReturn<T>? ResolveContext<T>(string? name = null) where T : class
        => ContextProvider.Resolve<T>(root, name);
}
