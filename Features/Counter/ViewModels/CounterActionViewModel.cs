using HelloAvalonia.Features.Counter.Contexts;
using HelloAvalonia.Framework.ViewModels;
using HelloAvalonia.UI.Adapters;
using R3;

namespace HelloAvalonia.Features.Counter.ViewModels;

public class CounterActionViewModel : ViewModelBase
{
    public IReadOnlyBindableReactiveProperty<bool> IsLoading { get; }
    public ReactiveCommand IncrementCommand { get; }
    public ReactiveCommand DecrementCommand { get; }
    public ReactiveCommand ResetCommand { get; }

    public CounterActionViewModel(CounterContext context, IDialogService dialogService)
    {
        IsLoading = context.IsLoading.ToReadOnlyBindableReactiveProperty().AddTo(Disposable);

        IncrementCommand = context.IsLoading
            .CombineLatest(context.Count, (isLoading, count) => !isLoading && count < int.MaxValue)
            .ToReactiveCommand()
            .AddTo(Disposable);

        DecrementCommand = context.IsLoading
            .CombineLatest(context.Count, (isLoading, count) => !isLoading && count > 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        ResetCommand = context.IsLoading
            .CombineLatest(context.Count, (isLoading, count) => !isLoading && count != 0)
            .ToReactiveCommand()
            .AddTo(Disposable);

        IncrementCommand
            .SubscribeAwait(async (_, ct) => await context.InvokeAsync(async innerCt =>
            {
                var current = context.Count.CurrentValue;
                await context.SetCountAsync(current + 1, TimeSpan.FromMilliseconds(100), innerCt);
            }))
            .AddTo(Disposable);

        DecrementCommand
            .SubscribeAwait(async (_, ct) => await context.InvokeAsync(async innerCt =>
            {
                var current = context.Count.CurrentValue;
                await context.SetCountAsync(current - 1, TimeSpan.FromMilliseconds(100), innerCt);
            }))
            .AddTo(Disposable);

        ResetCommand
            .SubscribeAwait(async (_, ct) => await context.InvokeAsync(async innerCt =>
            {
                var result = await dialogService.ShowDialogAsync(
                    "Reset Counter",
                    "Are you sure you want to reset the counter to zero?",
                    new YesNoDialogButtons("Yes", "No"),
                    innerCt);

                if (result == DialogResult.Yes)
                    await context.SetCountAsync(0, TimeSpan.FromMilliseconds(1000), innerCt);
            }))
            .AddTo(Disposable);
    }
}
