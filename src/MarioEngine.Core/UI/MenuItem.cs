namespace MarioEngine.Core.UI;

/// <summary>
/// A simple menu item with a label and click action.
/// </summary>
public sealed class MenuItem
{
    public string Label { get; set; } = string.Empty;
    public System.Action? Action { get; set; }
}
