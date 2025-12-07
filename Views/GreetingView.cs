using HelloAvalonia.ViewModels;
using HelloAvalonia.Views.Shared;
using HelloAvalonia.Views.ViewHosts;

namespace HelloAvalonia.Views;

public partial class GreetingView : UserControlBase<GreetingViewModel>
{
    public GreetingView()
    {
        InitializeComponent();
    }

    protected override void OnViewModelSet(GreetingViewModel viewModel)
    {
        _ = viewModel.AttachHostsAsync(new ContextViewHost(this));
    }
}
