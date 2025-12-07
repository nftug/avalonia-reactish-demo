using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Features.Greeting.Contexts;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Features.Greeting.ViewModels;

public partial class GreetingConsumerViewModel : ViewModelBase
{
    [ObservableProperty] private GreetingContext? context;

    public void AttachViewHosts(IContextViewHost viewHost)
    {
        (Context, _) = viewHost.RequireContext<GreetingContext>();

        Context.Text
            .Skip(1)
            .Subscribe(text =>
            {
                Console.WriteLine($"GreetingConsumerViewModel received text: {text}");
            })
            .AddTo(Disposable);
    }
}
