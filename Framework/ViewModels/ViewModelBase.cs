using CommunityToolkit.Mvvm.ComponentModel;
using R3;

namespace HelloAvalonia.Framework.ViewModels;

public abstract class ViewModelBase : ObservableObject, IDisposable
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
