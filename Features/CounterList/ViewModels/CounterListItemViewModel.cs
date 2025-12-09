using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public record CounterListItem(Guid Id, int Value)
{
    public static CounterListItem CreateNew(int value) => new(Guid.NewGuid(), value);
}

public class CounterListItemViewModel : ViewModelBase
{
    private readonly CounterListItem _model;

    public Guid Id => _model.Id;
    public int Value => _model.Value;
    public ReactiveCommand IncrementCommand { get; }
    public ReactiveCommand DecrementCommand { get; }

    public CounterListItemViewModel(CounterListItem model, Action<CounterListItem> onValueChanged)
    {
        _model = model;

        IncrementCommand = new ReactiveCommand().AddTo(Disposable);
        DecrementCommand = new ReactiveCommand().AddTo(Disposable);

        IncrementCommand.Subscribe(_ =>
        {
            onValueChanged(_model with { Value = Value + 1 });
        })
        .AddTo(Disposable);

        DecrementCommand.Subscribe(_ =>
        {
            onValueChanged(_model with { Value = Value - 1 });
        })
        .AddTo(Disposable);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Console.WriteLine($"CounterListItemViewModel Disposed: {Id}");
    }
}
