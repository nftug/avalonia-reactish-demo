using HelloAvalonia.Framework.Abstractions;
using ObservableCollections;

namespace HelloAvalonia.Features.CounterList.Models;

public class CounterListModel : BindableBase
{
    public ObservableList<CounterListItem> Counters { get; }

    public CounterListModel()
    {
        Counters = [.. Enumerable.Range(0, 5).Select(CounterListItem.CreateNew)];
    }
}
