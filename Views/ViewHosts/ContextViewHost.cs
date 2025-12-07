using Avalonia.Controls;
using HelloAvalonia.Adapters.Contexts;
using HelloAvalonia.Views.Common;

namespace HelloAvalonia.Views.ViewHosts;

public class ContextViewHost(Control root) : IContextViewHost
{
    public ContextReturn<T> RequireContext<T>(string? name = null) where T : class
        => ContextProvider.Require<T>(root, name);

    public ContextReturn<T>? ResolveContext<T>(string? name = null) where T : class
        => ContextProvider.Resolve<T>(root, name);

    public Task<ContextReturn<T>> RequireContextAsync<T>(string? name = null, TimeSpan? timeout = null) where T : class
        => ContextProvider.RequireAsync<T>(root, name, timeout);
}
