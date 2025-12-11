using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListItemViewModel : BindableBase
{
    private readonly CounterListItem _model;

    public int Value => _model.Value;

    public ReactiveCommand<Unit> IncrementCommand { get; }
    public ReactiveCommand<Unit> DecrementCommand { get; }

    public CounterListItemViewModel(CounterListItem model, Action<CounterListItem> updateModel)
    {
        _model = model;

        IncrementCommand = new ReactiveCommand<Unit>()
            .WithSubscribe(_ => updateModel(_model with { Value = Value + 1 }), Disposable);

        DecrementCommand = Observable.Return(Value > 0).ToReactiveCommand()
            .WithSubscribe(_ => updateModel(_model with { Value = Value - 1 }), Disposable);
    }
}
