using Avalonia;
using Avalonia.Controls;

namespace HelloAvalonia.Views.Shared;

public abstract class UserControlBase<TViewModel> : UserControl
    where TViewModel : class
{
    private bool _isViewModelSet;

    private void EnsureViewModelSet()
    {
        if (DataContext is TViewModel viewModel && !_isViewModelSet)
        {
            OnViewModelSet(viewModel);
            _isViewModelSet = true;
        }
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        EnsureViewModelSet();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        EnsureViewModelSet();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        (DataContext as IDisposable)?.Dispose();
    }

    protected virtual void OnViewModelSet(TViewModel viewModel)
    {
    }
}
