using HelloAvalonia.Framework.Contexts;
using R3;

namespace HelloAvalonia.Features.Greeting.Contexts;

public partial class GreetingContext : ContextBase
{
    public BindableReactiveProperty<string?> Text { get; }

    public GreetingContext()
    {
        Text = new BindableReactiveProperty<string?>().AddTo(Disposable);
    }
}
