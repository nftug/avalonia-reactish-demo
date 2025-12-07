using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.Utils;
using R3;

namespace HelloAvalonia.Framework.ViewModels;

public abstract class ViewModelBase : ObservableObject, IDisposable
{
    protected CompositeDisposable Disposable { get; } = [];

    public void Dispose()
    {
        Disposable.Dispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) { }

    public virtual void AttachViewHosts(IViewHost viewHost) { }

    public async Task InvokeAsync(Func<CancellationToken, Task> work)
        => await FrameworkUtils.InvokeAsync(Disposable, work);
}
