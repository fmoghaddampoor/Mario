namespace MarioEngine.Core.Levels;

/// <summary>Teleportation warp zone between levels.</summary>
internal sealed class WarpZone
{
    /// <summary>Entry point position.</summary>
    public Vector2 EntryPoint { get; set; }

    /// <summary>Target level name.</summary>
    public string TargetLevel { get; set; } = string.Empty;

    /// <summary>Exit point position in the target level.</summary>
    public Vector2 ExitPoint { get; set; }

    /// <summary>Whether this warp zone is one-way.</summary>
    public bool IsOneWay { get; set; }
}
