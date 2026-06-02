namespace MarioEngine.Core.Physics;

/// <summary>
/// Classification of a surface detected during collision.
/// Used by player controller for state-dependent behavior.
/// </summary>
internal enum SurfaceType
{
    /// <summary>No surface contact.</summary>
    None = 0,

    /// <summary>Standing on ground.</summary>
    Ground = 1,

    /// <summary>Pressing against a wall (left or right).</summary>
    Wall = 2,

    /// <summary>Hitting a ceiling above.</summary>
    Ceiling = 3,

    /// <summary>Standing on a one-way platform (can drop through).</summary>
    OneWayPlatform = 4,
}
