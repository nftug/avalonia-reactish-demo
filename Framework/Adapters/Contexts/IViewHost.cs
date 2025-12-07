namespace HelloAvalonia.Framework.Adapters.Contexts;

public interface IViewHost
{
    T? ResolveContext<T>(string? name = null) where T : class;
    T RequireContext<T>(string? name = null) where T : class;
}
