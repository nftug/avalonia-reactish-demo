using HelloAvalonia.Framework.ViewModels;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterPageViewModel(
    CounterDisplayViewModel displayViewModel,
    CounterActionViewModel actionViewModel
) : ViewModelBase
{
    public CounterDisplayViewModel DisplayViewModel { get; } = displayViewModel;

    public CounterActionViewModel ActionViewModel { get; } = actionViewModel;
}
