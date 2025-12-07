using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Features.Greeting.Contexts;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Features.Greeting.ViewModels;

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

    public void AttachViewHosts(IContextViewHost viewHost)
    {
        (Context, _) = viewHost.RequireContext<GreetingContext>();
    }
}
