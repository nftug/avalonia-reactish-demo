using HelloAvalonia.Framework.Abstractions;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterPageViewModel(
    CounterDisplayViewModel displayViewModel,
    CounterActionViewModel actionViewModel
) : BindableBase
{
    public CounterDisplayViewModel DisplayViewModel { get; } = displayViewModel;

    public CounterActionViewModel ActionViewModel { get; } = actionViewModel;
}
