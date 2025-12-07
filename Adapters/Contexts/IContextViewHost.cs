namespace HelloAvalonia.Adapters.Contexts;

public interface IContextViewHost
{
    ContextReturn<T>? ResolveContext<T>(string? name = null) where T : class;
    ContextReturn<T> RequireContext<T>(string? name = null) where T : class;
    Task<ContextReturn<T>> RequireContextAsync<T>(string? name = null, TimeSpan? timeout = null) where T : class;
}
