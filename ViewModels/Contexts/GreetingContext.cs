using HelloAvalonia.ViewModels.Shared;
using R3;

namespace HelloAvalonia.ViewModels.Contexts;

public partial class GreetingContext : ContextBase
{
    public BindableReactiveProperty<string?> Text { get; }

    public GreetingContext()
    {
        Text = new BindableReactiveProperty<string?>().AddTo(Disposable);
    }
}
