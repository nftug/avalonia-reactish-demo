using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using HelloAvalonia.Adapters.Contexts;
using R3;

namespace HelloAvalonia.Views.Common;

public class ContextProvider : ContentControl
{
    private CompositeDisposable _disposables = [];

    public static readonly StyledProperty<object?> ContextProperty =
        AvaloniaProperty.Register<ContextProvider, object?>(nameof(Context));

    public object? Context
    {
        get => GetValue(ContextProperty);
        set
        {
            if (value is IDisposable disposable) _disposables.Add(disposable);
            SetValue(ContextProperty, value);
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        _disposables.Dispose();
        _disposables = [];
    }

    public static ContextReturn<T>? Resolve<T>(Control control, string? name = null)
        where T : class
    {
        var provider = FindProvider<T>(control, name);
        return provider != null
            ? new ContextReturn<T>((T)provider.Context!, provider._disposables)
            : null;
    }

    public static ContextReturn<T> Require<T>(Control control, string? name = null)
        where T : class
    {
        var ctx = FindProvider<T>(control, name)
            ?? throw new InvalidOperationException($"Context<{typeof(T).Name}> not found.");
        return new ContextReturn<T>((T)ctx.Context!, ctx._disposables);
    }

    public static Task<ContextReturn<T>> RequireAsync<T>(
        Control control, string? name = null, TimeSpan? timeout = null)
        where T : class
    {
        timeout ??= TimeSpan.FromSeconds(10);

        var tcs = new TaskCompletionSource<ContextReturn<T>>();
        var cts = new CancellationTokenSource(timeout.Value);

        void Cleanup()
        {
            control.PropertyChanged -= CheckContext;
            cts.Dispose();
        }

        void CheckContext(object? _, object? __)
        {
            var ctx = FindProvider<T>(control, name);
            if (ctx != null)
            {
                if (tcs.TrySetResult(new ContextReturn<T>((T)ctx.Context!, ctx._disposables)))
                    Cleanup();
            }
        }

        cts.Token.Register(() =>
        {
            if (tcs.TrySetException(new TimeoutException(
                    $"Timeout while waiting for Context<{typeof(T).Name}>.")))
                Cleanup();
        });

        control.PropertyChanged += CheckContext;
        CheckContext(null, null); // initial

        return tcs.Task;
    }

    private static ContextProvider? FindProvider<T>(Control control, string? name)
        where T : class
    {
        return control.GetVisualAncestors()
            .OfType<ContextProvider>()
            .Where(x => x.Context is T)
            .FirstOrDefault(x => name == null || x.Name == name);
    }
}
