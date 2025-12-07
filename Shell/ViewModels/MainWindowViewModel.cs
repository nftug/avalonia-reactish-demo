using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Framework.ViewModels;

namespace HelloAvalonia.Shell.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public CounterContext CounterContext { get; } = new();

    public CounterPageViewModel CounterPageViewModel { get; } = new();
}
