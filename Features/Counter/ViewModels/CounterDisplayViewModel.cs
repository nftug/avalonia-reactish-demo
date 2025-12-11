using HelloAvalonia.Features.Counter.Models;
using HelloAvalonia.Framework.Abstractions;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterDisplayViewModel : BindableBase
{
    public IReadOnlyBindableReactiveProperty<int> Count { get; }
    public IReadOnlyBindableReactiveProperty<FizzBuzz> FizzBuzzState { get; }

    public CounterDisplayViewModel(CounterModel model)
    {
        Count = model.Count.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);

        FizzBuzzState = model.Count
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