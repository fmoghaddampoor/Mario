namespace MarioEngine.Core.UI;

using System;

/// <summary>Interactive UI button.</summary>
internal sealed class UIButton
{
    /// <summary>Button rectangle bounds.</summary>
    public Rect Bounds { get; set; }

    /// <summary>Button label text.</summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>Whether the button is enabled.</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>Whether the mouse is hovering.</summary>
    public bool IsHovered { get; private set; }

    /// <summary>Whether the button is pressed.</summary>
    public bool IsPressed { get; private set; }

    /// <summary>Raised on click.</summary>
    public event Action? OnClick;

    /// <summary>Renders the button.</summary>
    public void Render()
    {
    }
}
