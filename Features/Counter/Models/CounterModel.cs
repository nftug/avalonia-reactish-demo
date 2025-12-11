using HelloAvalonia.Framework.Abstractions;
using R3;

namespace HelloAvalonia.Features.Counter.Models;

public class CounterModel : BindableBase
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

    public CounterModel()
    {
        _count = new ReactiveProperty<int>(0).AddTo(Disposable);
        _isLoading = new ReactiveProperty<bool>(false).AddTo(Disposable);
    }

    public Task SetCountAsync(int newCount, TimeSpan delay, CancellationToken ct = default)
        => InvokeAsync(async innerCt =>
        {
            _isLoading.Value = true;

            try
            {
                var fetchedCount = await FetchDelayedCountAsync(newCount, delay, innerCt);
                _count.Value = fetchedCount;
            }
            finally
            {
                _isLoading.Value = false;
            }
        }, ct);
}
