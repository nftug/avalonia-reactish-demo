using HelloAvalonia.Framework.Utils;
using R3;

namespace HelloAvalonia.Framework.ViewModels;

public abstract class ViewModelBase : IDisposable
{
    protected CompositeDisposable Disposable { get; } = [];

    public void Dispose()
    {
        Disposable.Dispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) { }

    public async Task InvokeAsync(Func<CancellationToken, Task> work)
        => await FrameworkUtils.InvokeAsync(Disposable, work);
}
