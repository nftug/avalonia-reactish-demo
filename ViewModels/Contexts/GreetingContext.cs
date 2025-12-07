using HelloAvalonia.ViewModels.Shared;
using R3;

namespace HelloAvalonia.ViewModels.Contexts;

public class GreetingContext : ViewModelBase
{
    public BindableReactiveProperty<string?> Text { get; }

    public GreetingContext()
    {
        Text = new BindableReactiveProperty<string?>().AddTo(Disposable);
    }
}
