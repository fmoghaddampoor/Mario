namespace MarioEngine.Core.Levels;

/// <summary>Status of a level.</summary>
internal enum LevelStatus
{
    /// <summary>Level has not been started.</summary>
    NotStarted,

    /// <summary>Level is in progress.</summary>
    InProgress,

    /// <summary>Level has been completed.</summary>
    Completed,

    /// <summary>A secret has been found.</summary>
    SecretFound,
}

/// <summary>Tracks persistent state for a level.</summary>
internal sealed class LevelState
{
    /// <summary>Level name.</summary>
    public string LevelName { get; set; } = string.Empty;

    /// <summary>Current status.</summary>
    public LevelStatus Status { get; set; } = LevelStatus.NotStarted;

    /// <summary>Best score achieved.</summary>
    public int BestScore { get; set; }

    /// <summary>Best time achieved.</summary>
    public int BestTime { get; set; }

    /// <summary>Star coins collected.</summary>
    public int StarCoins { get; set; }
}
