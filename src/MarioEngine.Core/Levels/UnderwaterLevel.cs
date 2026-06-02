namespace MarioEngine.Core.Levels;

/// <summary>Level variant with underwater physics.</summary>
internal sealed class UnderwaterLevel : Level
{
    /// <summary>Y position of the water surface.</summary>
    public float WaterSurfaceY { get; set; }

    /// <summary>Buoyancy multiplier applied to physics.</summary>
    public float BuoyancyMultiplier { get; set; } = 0.5f;

    /// <summary>Returns true if the position is underwater.</summary>
    public bool IsUnderwater(Vector2 pos)
    {
        return pos.Y >= WaterSurfaceY;
    }
}
