using FluentAvalonia.Core;
using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListPageViewModel : DisposableBase
{
    private readonly ObservableList<CounterListItem> _counters;

    public DisposableViewListEnvelope<CounterListItem, CounterListItemViewModel> CountersViewEnvelope { get; }
    public IReadOnlyBindableReactiveProperty<int> CountersSum { get; }
    public ReactiveCommand AddCommand { get; }
    public ReactiveCommand RemoveCommand { get; }

    public CounterListPageViewModel()
    {
        _counters = [.. Enumerable.Range(0, 5).Select(CounterListItem.CreateNew)];

        CountersViewEnvelope = _counters
            .ToDisposableViewListEnvelope(
                model => new CounterListItemViewModel(model, updated => _counters.Update(updated, model)))
            .AddTo(Disposable);

        CountersSum = _counters
            .ObserveChangedWithPrepend()
            .Select(_ => _counters.Sum(c => c.Value))
            .ToReadOnlyBindableReactiveProperty()
            .AddTo(Disposable);

        AddCommand = new ReactiveCommand().AddTo(Disposable);
        RemoveCommand = _counters
            .ObserveCountChanged(notifyCurrentCount: true)
            .Select(count => count > 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        AddCommand.Subscribe(_ => HandleAddCounter()).AddTo(Disposable);
        RemoveCommand.Subscribe(_ => HandleRemoveCounter()).AddTo(Disposable);
    }

    private void HandleAddCounter()
    {
        var nextValue = _counters.Count > 0 ? _counters[^1].Value + 1 : 0;
        _counters.Add(CounterListItem.CreateNew(nextValue));
    }

    private void HandleRemoveCounter()
    {
        if (_counters.Count > 0)
        {
            var index = _counters.Count - 1;
            _counters.RemoveAt(index);
        }
    }
}
