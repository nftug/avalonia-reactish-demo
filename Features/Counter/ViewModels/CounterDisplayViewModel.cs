using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public partial class CounterDisplayViewModel : ViewModelBase
{
    private CounterContext? _context;

    [ObservableProperty] private IReadOnlyBindableReactiveProperty<int>? count;
    [ObservableProperty] private IReadOnlyBindableReactiveProperty<FizzBuzz>? fizzBuzzState;

    public override void AttachViewHosts(IViewHost viewHost)
    {
        _context = viewHost.RequireContext<CounterContext>();

        Count = _context.Count.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);

        FizzBuzzState = _context.Count
            .Select(count =>
                count switch
                {
                    0 => FizzBuzz.None,
                    _ when count % 15 == 0 => FizzBuzz.FizzBuzz,
                    _ when count % 3 == 0 => FizzBuzz.Fizz,
                    _ when count % 5 == 0 => FizzBuzz.Buzz,
                    _ => FizzBuzz.None
                })
            .ToReadOnlyBindableReactiveProperty()
            .AddTo(Disposable);
    }
}

public enum FizzBuzz
{
    Fizz,
    Buzz,
    FizzBuzz,
    None
}