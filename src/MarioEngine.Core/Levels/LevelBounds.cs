namespace MarioEngine.Core.Levels;

/// <summary>Defines rectangular bounds for a level.</summary>
internal sealed class LevelBounds
{
    /// <summary>Minimum X boundary.</summary>
    public float MinX { get; set; }

    /// <summary>Maximum X boundary.</summary>
    public float MaxX { get; set; }

    /// <summary>Minimum Y boundary.</summary>
    public float MinY { get; set; }

    /// <summary>Maximum Y boundary.</summary>
    public float MaxY { get; set; }

    /// <summary>Returns true if the position is within bounds.</summary>
    public bool IsInBounds(Vector2 pos)
    {
        return pos.X >= MinX && pos.X <= MaxX && pos.Y >= MinY && pos.Y <= MaxY;
    }

    /// <summary>Clamps a position to stay within bounds.</summary>
    public Vector2 ClampToBounds(Vector2 pos)
    {
        return new Vector2(
            Math.Clamp(pos.X, MinX, MaxX),
            Math.Clamp(pos.Y, MinY, MaxY)
        );
    }
}
