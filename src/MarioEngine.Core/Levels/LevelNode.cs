namespace MarioEngine.Core.Levels;

/// <summary>State of a level node on the world map.</summary>
internal enum LevelNodeState
{
    /// <summary>Level is locked.</summary>
    Locked,

    /// <summary>Level is unlocked.</summary>
    Unlocked,

    /// <summary>Level has been completed.</summary>
    Completed,

    /// <summary>Secret has been found.</summary>
    SecretFound,
}

/// <summary>A node representing a level on the world map.</summary>
internal sealed class LevelNode
{
    /// <summary>Position on the world map.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Associated level name.</summary>
    public string LevelName { get; set; } = string.Empty;

    /// <summary>Current node state.</summary>
    public LevelNodeState State { get; set; } = LevelNodeState.Locked;
}
