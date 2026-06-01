namespace MarioEngine.Core.Graphics.Font;

/// <summary>
/// Represents a single line of wrapped text with position and width.
/// </summary>
public sealed class TextLine
{
    /// <summary>Initializes a new instance of the <see cref="TextLine"/> class.</summary>
    /// <param name="text">The text content of this line.</param>
    /// <param name="width">Pixel width of this line.</param>
    /// <param name="y">Y position of this line in pixels.</param>
    public TextLine(string text, float width, float y)
    {
        Text = text;
        Width = width;
        Y = y;
    }

    /// <summary>Gets the text content of this line.</summary>
    public string Text { get; }

    /// <summary>Gets the pixel width of this line.</summary>
    public float Width { get; internal set; }

    /// <summary>Gets the Y position of this line in pixels.</summary>
    public float Y { get; }
}
