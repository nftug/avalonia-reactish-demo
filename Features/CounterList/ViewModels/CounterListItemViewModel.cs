using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Framework.Abstractions;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListItemViewModel : DisposableBase
{
    private readonly CounterListItem _model;

    public int Value => _model.Value;
    public ReactiveCommand IncrementCommand { get; }
    public ReactiveCommand DecrementCommand { get; }

    public CounterListItemViewModel(CounterListItem model, Action<CounterListItem> updateModel)
    {
        _model = model;

        IncrementCommand = new ReactiveCommand().AddTo(Disposable);
        DecrementCommand = Observable.Return(Value > 0).ToReactiveCommand().AddTo(Disposable);

        IncrementCommand
            .Subscribe(_ => updateModel(_model with { Value = Value + 1 }))
            .AddTo(Disposable);
        DecrementCommand
            .Subscribe(_ => updateModel(_model with { Value = Value - 1 }))
            .AddTo(Disposable);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Console.WriteLine($"CounterListItemViewModel Disposed: {_model.Id}");
    }
}
