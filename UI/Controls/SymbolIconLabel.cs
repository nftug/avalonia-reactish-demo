using Avalonia;
using Avalonia.Controls.Primitives;
using FluentAvalonia.UI.Controls;

namespace HelloAvalonia.UI.Controls;

public class SymbolIconLabel : TemplatedControl
{
    public static readonly StyledProperty<Symbol> SymbolProperty =
        AvaloniaProperty.Register<SymbolIconLabel, Symbol>(nameof(Symbol));

    public static readonly StyledProperty<string?> LabelProperty =
        AvaloniaProperty.Register<SymbolIconLabel, string?>(nameof(Label));

    public static readonly StyledProperty<double> SpacingProperty =
        AvaloniaProperty.Register<SymbolIconLabel, double>(nameof(Spacing), 10.0);

    public Symbol Symbol
    {
        get => GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public double Spacing
    {
        get => GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
}