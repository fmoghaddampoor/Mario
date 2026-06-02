namespace MarioEngine.Core.Levels;

/// <summary>Tracks scoring during a level.</summary>
internal sealed class LevelScore
{
    /// <summary>Current score.</summary>
    public int Score { get; set; }

    /// <summary>Coins collected.</summary>
    public int Coins { get; set; }

    /// <summary>Star coins collected.</summary>
    public int StarCoins { get; set; }

    /// <summary>Enemies defeated.</summary>
    public int EnemiesDefeated { get; set; }

    /// <summary>Calculates the final score with multipliers.</summary>
    public int CalculateFinalScore()
    {
        return Score + (Coins * 10) + (StarCoins * 100) + (EnemiesDefeated * 50);
    }

    /// <summary>Resets all values to zero.</summary>
    public void Reset()
    {
        Score = 0;
        Coins = 0;
        StarCoins = 0;
        EnemiesDefeated = 0;
    }
}
