using System.ComponentModel;
using System.Runtime.CompilerServices;
using R3;

namespace HelloAvalonia.ViewModels.Shared;

public abstract class ViewModelBase : IDisposable, INotifyPropertyChanged
{
    protected CompositeDisposable Disposable { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
        Disposable.Dispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) { }
}
