using Avalonia;
using Avalonia.Controls;
using HelloAvalonia.ViewModels.Shared;

namespace HelloAvalonia.Views.Shared;

public abstract class UserControlBase<TViewModel> : UserControl
    where TViewModel : ViewModelBase
{
    private void EnsureViewModelSet()
    {
        if (DataContext is TViewModel viewModel)
        {
            OnViewModelSet(viewModel);
        }
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
