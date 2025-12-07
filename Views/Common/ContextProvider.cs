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

    public static Context<T>? Resolve<T>(Control control, string? name = null)
        where T : class
    {
        var provider = FindProvider<T>(control, name);
        return provider != null
            ? new Context<T>((T)provider.Context!, provider._disposables)
            : null;
    }

    public static Context<T> Require<T>(Control control, string? name = null)
        where T : class
    {
        var ctx = FindProvider<T>(control, name)
            ?? throw new InvalidOperationException($"Context<{typeof(T).Name}> not found.");
        return new Context<T>((T)ctx.Context!, ctx._disposables);
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
