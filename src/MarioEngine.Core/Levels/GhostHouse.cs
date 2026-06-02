namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Level variant with haunted mechanics and random layout.</summary>
internal sealed class GhostHouse : Level
{
    /// <summary>Whether the ghost house is in haunted state.</summary>
    public bool IsHaunted { get; set; }

    /// <summary>Warp point positions for room transitions.</summary>
    public List<Vector2> WarpPoints { get; set; } = new();
}
