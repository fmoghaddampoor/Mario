namespace MarioEngine.Core.GamePlayer;

using System;

/// <summary>Manages the player's life count and game-over state.</summary>
public sealed class PlayerLives
{
    private int _lives = 3;
    private int _extraLifeThreshold = 100;

    /// <summary>Gets or sets the current number of lives.</summary>
    public int Lives
    {
        get => _lives;
        set => _lives = value;
    }

    /// <summary>Gets or sets the score threshold for earning an extra life.</summary>
    public int ExtraLifeThreshold
    {
        get => _extraLifeThreshold;
        set => _extraLifeThreshold = value;
    }

    /// <summary>Raised when the life count changes, providing the new count.</summary>
    public event Action<int>? OnLivesChanged;

    /// <summary>Raised when the player has no lives remaining.</summary>
    public event Action? OnGameOver;

    /// <summary>Removes one life. Returns <c>false</c> if the player is out of lives.</summary>
    /// <returns><c>true</c> if a life was lost and the player continues; <c>false</c> on game over.</returns>
    public bool LoseLife()
    {
        if (_lives <= 0)
            return false;

        _lives--;
        OnLivesChanged?.Invoke(_lives);

        if (_lives <= 0)
        {
            OnGameOver?.Invoke();
            return false;
        }

        return true;
    }

    /// <summary>Grants the player an extra life.</summary>
    public void GainLife()
    {
        _lives++;
        OnLivesChanged?.Invoke(_lives);
    }

    /// <summary>Resets lives to the default starting count.</summary>
    public void Reset()
    {
        _lives = 3;
        OnLivesChanged?.Invoke(_lives);
    }
}
