using HelloAvalonia.Features.Greeting.ViewModels;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Features.Greeting.Views;

public partial class GreetingConsumer : UserControlBase<GreetingConsumerViewModel>
{
    public GreetingConsumer()
    {
        InitializeComponent();
    }

    protected override void OnViewModelSet(GreetingConsumerViewModel viewModel)
    {
        viewModel.AttachViewHosts(new ContextViewHost(this));
    }
}
