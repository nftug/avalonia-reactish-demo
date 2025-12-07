using Avalonia.Controls;
using HelloAvalonia.Adapters.Contexts;
using HelloAvalonia.Views.Common;

namespace HelloAvalonia.Views.ViewHosts;

public class ContextViewHost(Control root) : IContextViewHost
{
    public Context<T> RequireContext<T>(string? name = null) where T : class
        => ContextProvider.Require<T>(root, name);

    public Context<T>? ResolveContext<T>(string? name = null) where T : class
        => ContextProvider.Resolve<T>(root, name);
}
