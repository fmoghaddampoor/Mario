namespace MarioEngine.Core.Levels;

using System;

/// <summary>Data snapshot when a level is completed.</summary>
internal sealed class LevelCompleteData
{
    /// <summary>Whether the level was completed.</summary>
    public bool Completed { get; set; }

    /// <summary>Final score.</summary>
    public int Score { get; set; }

    /// <summary>Coins collected.</summary>
    public int Coins { get; set; }

    /// <summary>Star coins collected.</summary>
    public int StarCoins { get; set; }

    /// <summary>Time remaining on the clock.</summary>
    public float TimeRemaining { get; set; }

    /// <summary>Number of deaths.</summary>
    public int Deaths { get; set; }

    /// <summary>Calculates a 0-3 star rating.</summary>
    public int CalculateStars()
    {
        if (!Completed)
            return 0;

        int stars = 1;

        if (StarCoins >= 3)
            stars++;

        if (Deaths == 0 && TimeRemaining > 60f)
            stars++;

        return Math.Clamp(stars, 0, 3);
    }
}
