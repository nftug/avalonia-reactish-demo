using FluentAvalonia.UI.Controls;
using HelloAvalonia.Framework.Utils;
using HelloAvalonia.UI.Adapters;

namespace HelloAvalonia.UI.Services;

public class DialogService : IDialogService
{
    public async Task<DialogResult> ShowDialogAsync(
        string title, string message, DialogButtons buttons, CancellationToken ct = default)
    {
        var window = FrameworkUtils.GetMainWindow();

        var dialog = new ContentDialog()
        {
            Title = title,
            Content = message
        };

        switch (buttons)
        {
            case OkDialogButtons okButtons:
                dialog.CloseButtonText = okButtons.Label;
                break;
            case YesNoDialogButtons yesNoButtons:
                dialog.PrimaryButtonText = yesNoButtons.YesLabel;
                dialog.SecondaryButtonText = yesNoButtons.NoLabel;
                break;
            case YesNoCancelDialogButtons yesNoCancelButtons:
                dialog.PrimaryButtonText = yesNoCancelButtons.YesLabel;
                dialog.SecondaryButtonText = yesNoCancelButtons.NoLabel;
                dialog.CloseButtonText = yesNoCancelButtons.CancelLabel;
                break;
        }

        ct.Register(dialog.Hide);
        var result = await dialog.ShowAsync(window);
        ct.ThrowIfCancellationRequested();

        return (buttons, result) switch
        {
            (OkDialogButtons _, _) => DialogResult.Ok,
            (_, ContentDialogResult.Primary) => DialogResult.Yes,
            (_, ContentDialogResult.Secondary) => DialogResult.No,
            _ => DialogResult.Cancel
        };
    }
}
