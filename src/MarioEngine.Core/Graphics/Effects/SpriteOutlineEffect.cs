namespace MarioEngine.Core.Graphics.Effects;

/// <summary>Applies an outline effect to sprite vertex data.</summary>
internal sealed class SpriteOutlineEffect
{
    public uint OutlineColor { get; set; } = 0x000000FF;
    public float OutlineWidth { get; set; } = 1f;
    public bool IsEnabled { get; set; } = true;

    public void ApplyToMesh(VertexData[] vertices)
    {
        if (!IsEnabled) return;
        // Expand vertices and assign outline color
    }
}

internal sealed class VertexData
{
    public float X { get; set; }
    public float Y { get; set; }
    public uint Color { get; set; }
}
