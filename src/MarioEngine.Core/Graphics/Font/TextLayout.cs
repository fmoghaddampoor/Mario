namespace MarioEngine.Core.Graphics.Font;

using System;
using System.Collections.Generic;

/// <summary>
/// Provides text measurement, wrapping, and layout calculations for <see cref="BitmapFont"/>.
/// </summary>
public static class TextLayout
{
    /// <summary>
    /// Measures the pixel width of a text string using the given font.
    /// Applies kerning between consecutive characters.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <param name="font">The bitmap font to use. Must not be null.</param>
    /// <returns>Total pixel width of the text.</returns>
    /// <exception cref="ArgumentNullException">Thrown if font is null.</exception>
    public static float MeasureText(string text, BitmapFont font)
    {
        ArgumentNullException.ThrowIfNull(font);
        if (string.IsNullOrEmpty(text))
        {
            return 0f;
        }

        var width = 0f;
        for (var i = 0; i < text.Length; i++)
        {
            var ch = font.GetCharacter(text[i]);
            if (ch == null)
            {
                continue;
            }

            if (i > 0)
            {
                width += font.GetKerning(text[i - 1], text[i]);
            }

            width += ch.AdvanceX;
        }

        return width;
    }

    /// <summary>
    /// Wraps text to fit within a maximum width by splitting on word boundaries.
    /// </summary>
    /// <param name="text">The text to wrap.</param>
    /// <param name="font">The bitmap font to use.</param>
    /// <param name="maxWidth">Maximum line width in pixels.</param>
    /// <param name="lineHeight">Line height in pixels.</param>
    /// <returns>List of wrapped text lines.</returns>
    /// <exception cref="ArgumentNullException">Thrown if font is null.</exception>
    public static IReadOnlyList<TextLine> WrapText(string text, BitmapFont font, float maxWidth, float lineHeight)
    {
        ArgumentNullException.ThrowIfNull(font);
        var lines = new List<TextLine>();
        if (string.IsNullOrEmpty(text))
        {
            return lines;
        }

        var words = text.Split(' ');
        var currentLine = string.Empty;
        var y = 0f;

        foreach (var word in words)
        {
            var testLine = currentLine.Length == 0 ? word : currentLine + " " + word;
            var testWidth = MeasureText(testLine, font);
            if (testWidth > maxWidth && currentLine.Length > 0)
            {
                lines.Add(new TextLine(currentLine, MeasureText(currentLine, font), y));
                y += lineHeight;
                currentLine = word;
            }
            else
            {
                currentLine = testLine;
            }
        }

        if (currentLine.Length > 0)
        {
            lines.Add(new TextLine(currentLine, MeasureText(currentLine, font), y));
        }

        return lines;
    }
}
