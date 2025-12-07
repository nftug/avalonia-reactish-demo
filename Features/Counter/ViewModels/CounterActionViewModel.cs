using CommunityToolkit.Mvvm.ComponentModel;
using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Framework.Adapters.Contexts;
using HelloAvalonia.Framework.ViewModels;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public partial class CounterActionViewModel : ViewModelBase
{
    private CounterContext? _context;

    [ObservableProperty] private IReadOnlyBindableReactiveProperty<bool>? isLoading;
    [ObservableProperty] private ReactiveCommand? incrementCommand;
    [ObservableProperty] private ReactiveCommand? decrementCommand;
    [ObservableProperty] private ReactiveCommand? resetCommand;

    public override void AttachViewHosts(IViewHost viewHost)
    {
        _context = viewHost.RequireContext<CounterContext>();

        IsLoading = _context.IsLoading.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);

        IncrementCommand = _context.IsLoading
            .CombineLatest(_context.Count, (isLoading, count) => !isLoading && count < int.MaxValue)
            .ToReactiveCommand()
            .AddTo(Disposable);

        DecrementCommand = _context.IsLoading
            .CombineLatest(_context.Count, (isLoading, count) => !isLoading && count > 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        ResetCommand = _context.IsLoading
            .CombineLatest(_context.Count, (isLoading, count) => !isLoading && count != 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        IncrementCommand
            .SubscribeAwait(async (_, ct) => await _context.InvokeAsync(async innerCt =>
            {
                var current = _context.Count.CurrentValue;
                await _context.SetCountAsync(current + 1, TimeSpan.FromMilliseconds(100), innerCt);
            }))
            .AddTo(Disposable);

        DecrementCommand
            .SubscribeAwait(async (_, ct) => await _context.InvokeAsync(async innerCt =>
            {
                var current = _context.Count.CurrentValue;
                await _context.SetCountAsync(current - 1, TimeSpan.FromMilliseconds(100), innerCt);
            }))
            .AddTo(Disposable);

        ResetCommand
            .SubscribeAwait(async (_, ct) => await _context.InvokeAsync(async innerCt =>
            {
                await _context.SetCountAsync(0, TimeSpan.FromMilliseconds(1000), innerCt);
            }))
            .AddTo(Disposable);
    }
}
