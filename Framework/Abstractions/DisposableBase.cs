using HelloAvalonia.Framework.Utils;
using R3;

namespace HelloAvalonia.Framework.Abstractions;

public abstract class DisposableBase : IDisposable
{
    private bool _disposed = false;
    protected CompositeDisposable Disposable { get; } = [];

    public void Dispose()
    {
        if (!_disposed)
        {
            Disposable.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }

    protected virtual void Dispose(bool disposing) { }

    public async Task InvokeAsync(Func<CancellationToken, Task> work)
        => await FrameworkUtils.InvokeAsync(Disposable, work);
}
