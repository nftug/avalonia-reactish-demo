using HelloAvalonia.Features.CounterList.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListPageViewModel : BindableBase
{
    private readonly CounterListModel _listModel;

    public BindableViewListEnvelope<CounterListItem, CounterListItemViewModel> CountersViewEnvelope { get; }
    public IReadOnlyBindableReactiveProperty<int> CountersSum { get; }

    public ReactiveCommand<Unit> AddCommand { get; }
    public ReactiveCommand<Unit> RemoveCommand { get; }
    public ReactiveCommand<Unit> SortAscendingCommand { get; }

    public CounterListPageViewModel(CounterListModel model)
    {
        _listModel = model;

        CountersViewEnvelope = _listModel.Counters
            .ToBindableViewListEnvelope<CounterListItem, CounterListItemViewModel>(
                model => new(model, updated => _listModel.Counters.Update(updated, model)))
            .AddTo(Disposable);

        CountersSum = _listModel.Counters
            .ObserveChangedWithPrepend()
            .Select(_ => _listModel.Counters.Sum(c => c.Value))
            .ToReadOnlyBindableReactiveProperty()
            .AddTo(Disposable);

        var countersCount = _listModel.Counters.ObserveCountChanged(notifyCurrentCount: true);

        AddCommand = new ReactiveCommand()
            .WithSubscribe(_ => HandleAddCounter(), Disposable);

        RemoveCommand = countersCount
            .Select(count => count > 0)
            .ToReactiveCommand()
            .WithSubscribe(_ => HandleRemoveCounter(), Disposable);

        SortAscendingCommand = countersCount
            .Select(count => count > 1)
            .ToReactiveCommand()
            .WithSubscribe(_ => HandleSortAscending(), Disposable);
    }

    private void HandleAddCounter()
    {
        var nextValue = _listModel.Counters.Count > 0 ? _listModel.Counters[^1].Value + 1 : 0;
        _listModel.Counters.Add(CounterListItem.CreateNew(nextValue));
    }

    private void HandleRemoveCounter()
    {
        if (_listModel.Counters.Count > 0)
        {
            var index = _listModel.Counters.Count - 1;
            _listModel.Counters.RemoveAt(index);
        }
    }

    private void HandleSortAscending()
    {
        _listModel.Counters.Sort(new CounterListItemComparer());
    }
}
