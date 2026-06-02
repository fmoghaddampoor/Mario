namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Level variant set inside a castle with interior lighting.</summary>
internal sealed class CastleLevel : Level
{
    /// <summary>Whether the castle interior is dark.</summary>
    public bool IsDark { get; set; }

    /// <summary>Positions of light sources in the castle.</summary>
    public List<Vector2> LightPositions { get; set; } = new();

    /// <summary>Ambient light level (0-1).</summary>
    public float AmbientLight { get; set; } = 0.3f;
}
