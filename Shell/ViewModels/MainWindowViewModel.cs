using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Features.Counter.ViewModels;
using HelloAvalonia.Framework.ViewModels;
using HelloAvalonia.UI.Adapters;
using HelloAvalonia.UI.Services;

namespace HelloAvalonia.Shell.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public IDialogService DialogService { get; } = new DialogService();

    public CounterContext CounterContext { get; } = new();

    public CounterPageViewModel CounterPageViewModel { get; } = new();
}
