namespace MarioEngine.Core.Graphics.Font;

/// <summary>Renders crisp text using signed distance field fonts.</summary>
internal sealed class DistanceFieldFont
{
    public int AtlasSize { get; init; } = 1024;

    public void Render(string text, Vector2 pos, float size, uint color)
    {
        // SDF glyph rendering — requires shader binding
    }
}

internal readonly struct Vector2(float x, float y)
{
    public float X { get; } = x;
    public float Y { get; } = y;
}
