namespace MarioEngine.Core.Core.Input;

/// <summary>Handles text input for UI fields.</summary>
internal sealed class TextInputHandler
{
    public string Text { get; private set; } = string.Empty;
    public bool IsActive { get; set; }
    public int MaxLength { get; set; } = 64;

    public void OnKeyPress(Key key, char character)
    {
        if (!IsActive) return;
        if (key == Key.Backspace && Text.Length > 0)
            Text = Text[..^1];
        else if (key == Key.Enter)
            IsActive = false;
        else if (character != 0 && Text.Length < MaxLength)
            Text += character;
    }
}
