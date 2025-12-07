namespace HelloAvalonia.Framework.Adapters.Contexts;

public interface IContextViewHost
{
    ContextReturn<T>? ResolveContext<T>(string? name = null) where T : class;
    ContextReturn<T> RequireContext<T>(string? name = null) where T : class;
}
