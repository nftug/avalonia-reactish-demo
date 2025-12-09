using HelloAvalonia.Framework.Abstractions;
using R3;

namespace HelloAvalonia.Features.Counter.Contexts;

public class CounterContext : DisposableBase
{
    private readonly ReactiveProperty<int> _count;
    private readonly ReactiveProperty<bool> _isLoading;

    public ReadOnlyReactiveProperty<int> Count => _count;
    public ReadOnlyReactiveProperty<bool> IsLoading => _isLoading;

    private static async Task<int> FetchDelayedCountAsync(int newCount, TimeSpan delay, CancellationToken ct)
    {
        await Task.Delay(delay, ct);
        return newCount;
    }

    public CounterContext()
    {
        _count = new ReactiveProperty<int>(0).AddTo(Disposable);
        _isLoading = new ReactiveProperty<bool>(false).AddTo(Disposable);
    }

    public async Task SetCountAsync(int newCount, TimeSpan delay, CancellationToken ct = default)
    {
        _isLoading.Value = true;
        try
        {
            int fetchedCount = await FetchDelayedCountAsync(newCount, delay, ct);
            _count.Value = fetchedCount;
        }
        finally
        {
            _isLoading.Value = false;
        }
    }
}
