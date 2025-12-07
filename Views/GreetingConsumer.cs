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
        _ = viewModel.AttachHostsAsync(new ContextViewHost(this));
    }
}
