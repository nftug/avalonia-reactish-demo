using Avalonia;
using HelloAvalonia.ViewModels;
using HelloAvalonia.Views.Shared;
using HelloAvalonia.Views.ViewHosts;

namespace HelloAvalonia.Views;

public partial class GreetingConsumer : UserControlBase<GreetingConsumerViewModel>
{
    public GreetingConsumer()
    {
        InitializeComponent();
    }

    protected override void OnViewModelSet(GreetingConsumerViewModel viewModel)
    {
        // viewModel.AttachHosts(new ContextViewHost(this));
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if (DataContext is GreetingConsumerViewModel viewModel)
        {
            viewModel.AttachHosts(new ContextViewHost(this));
        }
    }
}
