namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Serializable save data for a level.</summary>
internal sealed class LevelSaveData
{
    /// <summary>World number.</summary>
    public int WorldNumber { get; set; }

    /// <summary>Level number.</summary>
    public int LevelNumber { get; set; }

    /// <summary>Current score.</summary>
    public int Score { get; set; }

    /// <summary>Coins collected.</summary>
    public int Coins { get; set; }

    /// <summary>Lives remaining.</summary>
    public int Lives { get; set; }

    /// <summary>Player position.</summary>
    public Vector2 PlayerPosition { get; set; }

    /// <summary>Time remaining on the clock.</summary>
    public float TimeRemaining { get; set; }

    /// <summary>Collected item identifiers.</summary>
    public List<string> CollectedItems { get; set; } = new();
}
