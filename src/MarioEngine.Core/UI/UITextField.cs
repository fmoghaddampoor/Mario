namespace MarioEngine.Core.UI;

/// <summary>Text input field for rename/input operations.</summary>
internal sealed class UITextField
{
    /// <summary>Field rectangle bounds.</summary>
    public Rect Bounds { get; set; }

    /// <summary>Current text content.</summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>Placeholder text when empty.</summary>
    public string Placeholder { get; set; } = string.Empty;

    /// <summary>Whether the field is focused.</summary>
    public bool IsFocused { get; set; }

    /// <summary>Handles key press input.</summary>
    public void OnKeyPress(Key key)
    {
    }
}
