using StrongInject;

namespace HelloAvalonia.Framework.Adapters.Contexts;

public interface IOwnedContext<out T> : IDisposable
    where T : notnull
{
    T Value { get; }
}

public sealed class OwnedContext<T>(IOwned<T> inner) : IOwnedContext<T>
    where T : notnull
{
    public T Value => inner.Value;

    public void Dispose() => inner.Dispose();
}
