using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListPageViewModel : BindableBase
{
    private readonly CounterListModel _model;

    public BindableViewListEnvelope<CounterListItem, CounterListItemViewModel> CountersViewEnvelope { get; }
    public IReadOnlyBindableReactiveProperty<int> CountersSum { get; }
    public ReactiveCommand<Unit> AddCommand { get; }
    public ReactiveCommand<Unit> RemoveCommand { get; }
    public ReactiveCommand<Unit> SortAscendingCommand { get; }

    public CounterListPageViewModel(CounterListModel model)
    {
        _model = model;

        CountersViewEnvelope = _model.Counters
            .ToBindableViewListEnvelope(
                model => new CounterListItemViewModel(model, updated => _model.Counters.Update(updated, model)))
            .AddTo(Disposable);

        CountersSum = _model.Counters
            .ObserveChangedWithPrepend()
            .Select(_ => _model.Counters.Sum(c => c.Value))
            .ToReadOnlyBindableReactiveProperty()
            .AddTo(Disposable);

        var countersCount = _model.Counters.ObserveCountChanged(notifyCurrentCount: true);

        AddCommand = new ReactiveCommand()
            .WithSubscribe(_ => HandleAddCounter(), Disposable);

        RemoveCommand = countersCount.Select(count => count > 0).ToReactiveCommand()
            .WithSubscribe(_ => HandleRemoveCounter(), Disposable);

        SortAscendingCommand = countersCount.Select(count => count > 1).ToReactiveCommand()
            .WithSubscribe(_ => HandleSortCounters(), Disposable);
    }

    private void HandleAddCounter()
    {
        var nextValue = _model.Counters.Count > 0 ? _model.Counters[^1].Value + 1 : 0;
        _model.Counters.Add(CounterListItem.CreateNew(nextValue));
    }

    private void HandleRemoveCounter()
    {
        if (_model.Counters.Count > 0)
        {
            var index = _model.Counters.Count - 1;
            _model.Counters.RemoveAt(index);
        }
    }

    private void HandleSortCounters()
    {
        _model.Counters.Sort(new CounterListItemComparer());
    }
}
