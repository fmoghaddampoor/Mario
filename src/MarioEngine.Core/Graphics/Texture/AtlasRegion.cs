namespace MarioEngine.Core.Graphics;

/// <summary>
/// Defines a single region within a texture atlas.
/// Contains pixel bounds and normalized UV coordinates for rendering.
/// </summary>
public sealed class AtlasRegion
{
    /// <summary>Gets the region name (e.g. "mario_run_01").</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the X position in the atlas in pixels.</summary>
    public int X { get; init; }

    /// <summary>Gets the Y position in the atlas in pixels.</summary>
    public int Y { get; init; }

    /// <summary>Gets the region width in pixels.</summary>
    public int Width { get; init; }

    /// <summary>Gets the region height in pixels.</summary>
    public int Height { get; init; }

    /// <summary>Gets the normalized UV left coordinate (0-1).</summary>
    public float U1 { get; init; }

    /// <summary>Gets the normalized UV top coordinate (0-1).</summary>
    public float V1 { get; init; }

    /// <summary>Gets the normalized UV right coordinate (0-1).</summary>
    public float U2 { get; init; }

    /// <summary>Gets the normalized UV bottom coordinate (0-1).</summary>
    public float V2 { get; init; }
}
