using R3;

namespace HelloAvalonia.Framework.Contexts;

public abstract class ContextBase : IDisposable
{
    protected CompositeDisposable Disposable { get; } = new();

    public void Dispose()
    {
        Disposable.Dispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) { }
}