using HelloAvalonia.Features.Greeting.ViewModels;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.Views;

namespace HelloAvalonia.Features.Greeting.Views;

public partial class GreetingView : UserControlBase<GreetingViewModel>
{
    public GreetingView()
    {
        InitializeComponent();
    }

    protected override void OnViewModelSet(GreetingViewModel viewModel)
    {
        viewModel.AttachViewHosts(new ContextViewHost(this));
    }
}
