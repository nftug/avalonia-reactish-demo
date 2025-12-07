using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Adapters.Contexts;
using HelloAvalonia.ViewModels.Contexts;
using HelloAvalonia.ViewModels.Shared;
using R3;

namespace HelloAvalonia.ViewModels;

public partial class GreetingViewModel : ViewModelBase
{
    [ObservableProperty] private GreetingContext? context;
    public IReadOnlyBindableReactiveProperty<string?> Greeting { get; }
    public GreetingConsumerViewModel GreetingConsumerViewModel { get; }

    public GreetingViewModel(Observable<string?> greeting)
    {
        Greeting = greeting.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);
        GreetingConsumerViewModel = new GreetingConsumerViewModel();
    }

    public async Task AttachHostsAsync(IContextViewHost viewHost)
    {
        (Context, _) = await viewHost.RequireContextAsync<GreetingContext>();
    }
}
