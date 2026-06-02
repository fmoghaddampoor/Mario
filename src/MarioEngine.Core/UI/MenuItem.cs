namespace MarioEngine.Core.UI;

/// <summary>
/// A simple menu item with a label and click action.
/// </summary>
internal sealed class MenuItem
{
    internal string Label { get; set; } = string.Empty;
    internal System.Action? Action { get; set; }
}
