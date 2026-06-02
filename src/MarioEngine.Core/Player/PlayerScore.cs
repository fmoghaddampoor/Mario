namespace MarioEngine.Core.Player;

using System;

/// <summary>Manages the player's score, high score, and score multiplier.</summary>
internal sealed class PlayerScore
{
    /// <summary>Gets the current score.</summary>
    public int Score { get; private set; }

    /// <summary>Gets the highest score achieved.</summary>
    public int HighScore { get; private set; }

    /// <summary>Gets the current score multiplier.</summary>
    public int Multiplier { get; private set; } = 1;

    /// <summary>Raised when the score changes, providing the new score.</summary>
    public event Action<int>? OnScoreChanged;

    /// <summary>Adds points to the score, multiplied by the current multiplier.</summary>
    /// <param name="points">Base points to add before multiplier.</param>
    public void AddScore(int points)
    {
        int total = points * Multiplier;
        Score += total;
        if (Score > HighScore)
            HighScore = Score;
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>Resets the multiplier back to 1.</summary>
    public void ResetMultiplier()
    {
        Multiplier = 1;
    }
}
