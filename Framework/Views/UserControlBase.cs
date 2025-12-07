using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;

namespace HelloAvalonia.Framework.Views;

public abstract class UserControlBase<TViewModel> : UserControl
    where TViewModel : class
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
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        (DataContext as IDisposable)?.Dispose();
    }
}
