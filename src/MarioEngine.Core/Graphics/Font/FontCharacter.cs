namespace MarioEngine.Core.Graphics.Font;

/// <summary>
/// Defines a single character in a bitmap font atlas.
/// Contains UV coordinates, size, offset, and advance for rendering.
/// </summary>
public sealed class FontCharacter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FontCharacter"/> class.
    /// </summary>
    /// <param name="id">ASCII character code (32-127).</param>
    /// <param name="x">X position in the atlas texture in pixels.</param>
    /// <param name="y">Y position in the atlas texture in pixels.</param>
    /// <param name="width">Character width in pixels.</param>
    /// <param name="height">Character height in pixels.</param>
    /// <param name="offsetX">Horizontal render offset in pixels.</param>
    /// <param name="offsetY">Vertical render offset in pixels.</param>
    /// <param name="advanceX">Horizontal advance in pixels.</param>
    /// <param name="textureWidth">Atlas texture width for UV calculation.</param>
    /// <param name="textureHeight">Atlas texture height for UV calculation.</param>
    public FontCharacter(
        int id,
        int x,
        int y,
        int width,
        int height,
        int offsetX,
        int offsetY,
        int advanceX,
        int textureWidth,
        int textureHeight)
    {
        Id = id;
        Width = width;
        Height = height;
        OffsetX = offsetX;
        OffsetY = offsetY;
        AdvanceX = advanceX;
        U1 = (float)x / textureWidth;
        V1 = (float)y / textureHeight;
        U2 = (float)(x + width) / textureWidth;
        V2 = (float)(y + height) / textureHeight;
    }

    /// <summary>Gets the ASCII character code.</summary>
    public int Id { get; }

    /// <summary>Gets the character width in pixels.</summary>
    public int Width { get; }

    /// <summary>Gets the character height in pixels.</summary>
    public int Height { get; }

    /// <summary>Gets the horizontal render offset in pixels.</summary>
    public int OffsetX { get; }

    /// <summary>Gets the vertical render offset in pixels.</summary>
    public int OffsetY { get; }

    /// <summary>Gets the horizontal advance for the next character.</summary>
    public int AdvanceX { get; }

    /// <summary>Gets the left UV coordinate (0-1).</summary>
    public float U1 { get; }

    /// <summary>Gets the top UV coordinate (0-1).</summary>
    public float V1 { get; }

    /// <summary>Gets the right UV coordinate (0-1).</summary>
    public float U2 { get; }

    /// <summary>Gets the bottom UV coordinate (0-1).</summary>
    public float V2 { get; }
}
