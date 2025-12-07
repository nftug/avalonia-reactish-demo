using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.ViewModels;

namespace HelloAvalonia.Framework.Views;

public abstract class UserControlBase<TViewModel> : UserControl
    where TViewModel : ViewModelBase
{
    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is TViewModel viewModel)
        {
            Dispatcher.UIThread.Post(() => OnViewModelSet(viewModel), DispatcherPriority.Loaded);
        }
    }

    protected virtual void OnViewModelSet(TViewModel viewModel)
    {
        viewModel.AttachViewHosts(new ViewHost(this));
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        (DataContext as IDisposable)?.Dispose();
    }
}
