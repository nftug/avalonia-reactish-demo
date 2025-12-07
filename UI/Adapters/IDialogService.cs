namespace HelloAvalonia.UI.Adapters;

public interface IDialogService
{
    Task<DialogResult> ShowDialogAsync(
        string title,
        string message,
        DialogButtons buttons,
        CancellationToken ct = default);
}

public abstract record DialogButtons;

public record OkDialogButtons(string Label = "OK") : DialogButtons;

public record YesNoDialogButtons(string YesLabel = "Yes", string NoLabel = "No") : DialogButtons;

public record YesNoCancelDialogButtons(
    string YesLabel = "Yes", string NoLabel = "No", string CancelLabel = "Cancel") : DialogButtons;

public enum DialogResult
{
    Ok,
    Yes,
    No,
    Cancel
}
