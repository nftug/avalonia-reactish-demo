using Avalonia;
using Avalonia.Controls;
using HelloAvalonia.Framework.Abstractions;
using R3;

namespace HelloAvalonia.Framework.Views;

public abstract class UserControlBase<TViewModel> : UserControl
    where TViewModel : BindableBase
{
    private bool _isAttached;
    private TViewModel? _viewModel;
    protected CompositeDisposable Disposable { get; } = [];

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _isAttached = true;
        TryAttach();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _isAttached = false;
        _viewModel = null;
        (DataContext as IDisposable)?.Dispose();
        Disposable.Dispose();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        _viewModel = DataContext as TViewModel;
        TryAttach();
    }

    private void TryAttach()
    {
        if (_isAttached && _viewModel is not null)
            OnAfterRender(_viewModel);
    }

    protected virtual void OnAfterRender(TViewModel viewModel)
    {
    }
}