using HelloAvalonia.Framework.Views;
using HelloAvalonia.UI.Navigation.ViewModels;
using R3;

namespace HelloAvalonia.UI.Navigation.Views;

public partial class NavigationView : UserControlBase<NavigationViewModel>
{
    public NavigationView()
    {
        InitializeComponent();
    }

    protected override void OnAfterRender(NavigationViewModel viewModel)
    {
        viewModel.NavigateRequested
             .Subscribe(t => ContentFrame.NavigateFromObject(t))
             .AddTo(Disposable);
    }
}
