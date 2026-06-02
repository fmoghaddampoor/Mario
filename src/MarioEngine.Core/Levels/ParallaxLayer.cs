namespace MarioEngine.Core.Levels;

/// <summary>Parallax scrolling layer for backgrounds.</summary>
internal sealed class ParallaxLayer
{
    /// <summary>Scroll speed multiplier per axis.</summary>
    public Vector2 SpeedMultiplier { get; set; }

    /// <summary>Current X scroll offset.</summary>
    public float ScrollX { get; set; }

    /// <summary>Current Y scroll offset.</summary>
    public float ScrollY { get; set; }

    /// <summary>Gets the offset for a given camera position.</summary>
    public Vector2 GetOffset(float cameraX, float cameraY)
    {
        return new Vector2(
            cameraX * SpeedMultiplier.X + ScrollX,
            cameraY * SpeedMultiplier.Y + ScrollY
        );
    }
}
