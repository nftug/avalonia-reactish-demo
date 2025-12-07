using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using R3;

namespace HelloAvalonia.Framework.Views;

public class ContextProvider : ContentControl
{
    public static readonly StyledProperty<object?> ContextProperty =
        AvaloniaProperty.Register<ContextProvider, object?>(nameof(Context));

    public object? Context
    {
        get => GetValue(ContextProperty);
        set => SetValue(ContextProperty, value);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        (Context as IDisposable)?.Dispose();
    }

    public static T? Resolve<T>(Control control, string? name = null)
        where T : class
    {
        var provider = FindProvider<T>(control, name);
        return provider?.Context as T;
    }

    public static T Require<T>(Control control, string? name = null)
        where T : class
    {
        var ctx = FindProvider<T>(control, name)
            ?? throw new InvalidOperationException($"Context<{typeof(T).Name}> not found.");
        return (T)ctx.Context!;
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
