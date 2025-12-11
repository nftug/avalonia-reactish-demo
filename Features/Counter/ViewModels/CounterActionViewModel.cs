using HelloAvalonia.Features.Counter.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using HelloAvalonia.UI.Adapters;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterActionViewModel : BindableBase
{
    private readonly CounterModel _model;

    public IReadOnlyBindableReactiveProperty<bool> IsLoading { get; }

    public ReactiveCommand<Unit> IncrementCommand { get; }
    public ReactiveCommand<Unit> DecrementCommand { get; }
    public ReactiveCommand<Unit> ResetCommand { get; }

    public CounterActionViewModel(CounterModel model, IDialogService dialogService)
    {
        _model = model;

        IsLoading = _model.IsLoading.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);

        IncrementCommand = _model.IsLoading
            .CombineLatest(_model.Count, (isLoading, count) => !isLoading && count < int.MaxValue)
            .ToReactiveCommand()
            .WithSubscribeAwait(async (_, _) => await HandleIncrementAsync(), Disposable);

        DecrementCommand = model.IsLoading
            .CombineLatest(_model.Count, (isLoading, count) => !isLoading && count > 0)
            .ToReactiveCommand()
            .WithSubscribeAwait(async (_, _) => await HandleDecrementAsync(), Disposable);

        ResetCommand = _model.IsLoading
            .CombineLatest(_model.Count, (isLoading, count) => !isLoading && count != 0)
            .ToReactiveCommand()
            .WithSubscribeAwait(async (_, _) => await HandleResetAsync(dialogService), Disposable);
    }

    private Task HandleIncrementAsync(CancellationToken ct = default)
    {
        var current = _model.Count.CurrentValue;
        return _model.SetCountAsync(current + 1, TimeSpan.FromMilliseconds(100), ct);
    }

    private Task HandleDecrementAsync(CancellationToken ct = default)
    {
        var current = _model.Count.CurrentValue;
        return _model.SetCountAsync(current - 1, TimeSpan.FromMilliseconds(100), ct);
    }

    private async Task HandleResetAsync(IDialogService dialogService, CancellationToken ct = default)
    {
        var result = await dialogService.ShowDialogAsync(
            "Reset Counter",
            "Are you sure you want to reset the counter to zero?",
            new YesNoDialogButtons("Yes", "No"),
            ct);

        if (result == DialogResult.Yes)
        {
            await _model.SetCountAsync(0, TimeSpan.FromMilliseconds(3000), ct);
        }
    }
}
