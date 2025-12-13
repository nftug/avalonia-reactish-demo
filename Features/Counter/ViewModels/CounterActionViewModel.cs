using HelloAvalonia.Features.Counter.Models;
using HelloAvalonia.Framework.Abstractions;
using HelloAvalonia.Framework.Utils;
using HelloAvalonia.UI.Adapters;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterActionViewModel : BindableBase
{
    private readonly CounterModel _model;
    private readonly IDialogService _dialogService;

    public IReadOnlyBindableReactiveProperty<bool> IsLoading { get; }

    public ReactiveCommand<Unit> IncrementCommand { get; }
    public ReactiveCommand<Unit> DecrementCommand { get; }
    public ReactiveCommand<Unit> ResetCommand { get; }

    public CounterActionViewModel(CounterModel model, IDialogService dialogService)
    {
        _model = model;
        _dialogService = dialogService;

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
            .WithSubscribeAwait(async (_, _) => await HandleResetAsync(), Disposable);
    }

    private Task HandleIncrementAsync()
    {
        var current = _model.Count.CurrentValue;
        return _model.SetCountAsync(current + 1, TimeSpan.FromMilliseconds(100));
    }

    private Task HandleDecrementAsync()
    {
        var current = _model.Count.CurrentValue;
        return _model.SetCountAsync(current - 1, TimeSpan.FromMilliseconds(100));
    }

    private async Task HandleResetAsync()
    {
        var result = await _dialogService.ShowDialogAsync(
            "Reset Counter",
            "Are you sure you want to reset the counter to zero?",
            new YesNoDialogButtons("Yes", "No")
        );

        if (result == DialogResult.Yes)
        {
            await _model.SetCountAsync(0, TimeSpan.FromMilliseconds(3000));
        }
    }
}
