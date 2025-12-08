using HelloAvalonia.Framework.Views;
using HelloAvalonia.UI.Navigation.ViewModels;
using R3;

namespace HelloAvalonia.UI.Navigation.Views;

public partial class NavigationView : UserControlBase<NavigationViewModel>
{
    public NavigationView()
    {
        InitializeComponent();

        // To prevent transition animation flicker on initial load
        ContentFrame.IsVisible = false;
    }

    protected override void OnViewModelSet(NavigationViewModel viewModel)
    {
        base.OnViewModelSet(viewModel);

        viewModel.NavigateRequested
            .Subscribe(t =>
            {
                ContentFrame.NavigateFromObject(t);
                ContentFrame.IsVisible = true;
            })
            .AddTo(Disposable);
    }
}