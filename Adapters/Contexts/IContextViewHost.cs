namespace HelloAvalonia.Adapters.Contexts;

public interface IContextViewHost
{
    Context<T>? ResolveContext<T>(string? name = null) where T : class;
    Context<T> RequireContext<T>(string? name = null) where T : class;
}
