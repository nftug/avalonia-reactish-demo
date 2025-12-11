using ObservableCollections;
using R3;

namespace HelloAvalonia.Framework.Abstractions;

public sealed class BindableViewListEnvelope<T, TView> : IDisposable
    where TView : IDisposable
{
    private readonly ISynchronizedView<T, TView> _synchronizedView;
    private readonly CompositeDisposable _disposables = [];

    public INotifyCollectionChangedSynchronizedViewList<TView> View { get; }

    public BindableViewListEnvelope(ObservableList<T> source, Func<T, TView> transform)
    {
        _synchronizedView = source.CreateView(transform).AddTo(_disposables);

        _synchronizedView
             .ObserveChanged()
             .Where(e => e.OldItem.View is not null)
             .Subscribe(e => e.OldItem.View.Dispose())
             .AddTo(_disposables);

        _synchronizedView
            .ObserveReset()
            .Where(e => e.IsClear)
            .Subscribe(e => _synchronizedView.ToList().ForEach(v => v.Dispose()))
            .AddTo(_disposables);

        Disposable
            .Create(() => _synchronizedView.ToList().ForEach(v => v.Dispose()))
            .AddTo(_disposables);

        View = _synchronizedView.ToNotifyCollectionChanged().AddTo(_disposables);
    }

    public void Dispose() => _disposables.Dispose();
}

public static class BindableViewListEnvelopeExtensions
{
    public static BindableViewListEnvelope<T, TView> ToBindableViewListEnvelope<T, TView>(
        this ObservableList<T> source,
        Func<T, TView> transform)
        where TView : IDisposable
    {
        return new BindableViewListEnvelope<T, TView>(source, transform);
    }

    public static void Update<T>(this ObservableList<T> source, T updated, T origin)
        where T : notnull
    {
        var index = source.IndexOf(origin);
        if (index < 0) return;
        source[index] = updated;
    }
}