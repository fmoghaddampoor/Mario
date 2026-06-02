namespace MarioEngine.Core.Levels;

/// <summary>Difficulty levels for the game.</summary>
internal enum Difficulty
{
    /// <summary>Easy difficulty.</summary>
    Easy,

    /// <summary>Normal difficulty.</summary>
    Normal,

    /// <summary>Hard difficulty.</summary>
    Hard,

    /// <summary>Expert difficulty.</summary>
    Expert,
}

/// <summary>Helper methods for difficulty multipliers.</summary>
internal static class LevelDifficulty
{
    /// <summary>Returns the speed multiplier for the given difficulty.</summary>
    public static float GetSpeedMultiplier(Difficulty d)
    {
        return d switch
        {
            Difficulty.Easy => 0.75f,
            Difficulty.Normal => 1.0f,
            Difficulty.Hard => 1.25f,
            Difficulty.Expert => 1.5f,
            _ => 1.0f,
        };
    }

    /// <summary>Returns the enemy count multiplier for the given difficulty.</summary>
    public static int GetEnemyCountMultiplier(Difficulty d)
    {
        return d switch
        {
            Difficulty.Easy => 1,
            Difficulty.Normal => 2,
            Difficulty.Hard => 3,
            Difficulty.Expert => 4,
            _ => 1,
        };
    }
}
