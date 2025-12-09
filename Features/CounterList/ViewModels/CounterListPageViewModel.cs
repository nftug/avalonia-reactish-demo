using FluentAvalonia.Core;
using HelloAvalonia.Framework.Utils;
using HelloAvalonia.Framework.ViewModels;
using ObservableCollections;
using R3;

namespace HelloAvalonia.Features.CounterList.ViewModels;

public class CounterListPageViewModel : ViewModelBase
{
    private readonly ObservableList<CounterListItem> _counters;

    public NotifyCollectionChangedSynchronizedViewList<CounterListItemViewModel> CountersView { get; }
    public IReadOnlyBindableReactiveProperty<int> CountersSum { get; }
    public ReactiveCommand AddCommand { get; }
    public ReactiveCommand RemoveCommand { get; }

    public CounterListPageViewModel()
    {
        _counters = [.. Enumerable.Range(0, 5).Select(i => CounterListItem.CreateNew(i))];

        var countersView = _counters
            .CreateViewModelView(
                item => new CounterListItemViewModel(item, OnItemValueChanged),
                Disposable
            );

        CountersView = countersView.ToNotifyCollectionChanged().AddTo(Disposable);

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

    private void OnItemValueChanged(CounterListItem item)
    {
        if (_counters.FirstOrDefault(c => c.Id == item.Id) is { } existingItem)
        {
            var index = _counters.IndexOf(existingItem);
            _counters[index] = item;
        }
    }
}
