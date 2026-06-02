namespace MarioEngine.Core.GamePlayer;

using System;

/// <summary>Handles the player death sequence, animation timing, and completion events.</summary>
public sealed class PlayerDeathHandler
{
    /// <summary>Gets whether the player is currently in the death state.</summary>
    public bool IsDying { get; private set; }

    /// <summary>Gets or sets the duration of the death animation in seconds.</summary>
    public float DeathAnimationDuration { get; set; } = 1.5f;

    /// <summary>Gets the elapsed time since death started.</summary>
    public float DeathTimer { get; private set; }

    /// <summary>Raised when the death animation has completed.</summary>
    public event Action? OnDeathAnimationComplete;

    /// <summary>Starts the death sequence, resetting the death timer.</summary>
    public void StartDeath()
    {
        IsDying = true;
        DeathTimer = 0f;
    }

    /// <summary>Advances the death timer by the given delta time.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        if (!IsDying)
            return;

        DeathTimer += dt;

        if (DeathTimer >= DeathAnimationDuration)
        {
            IsDying = false;
            OnDeathAnimationComplete?.Invoke();
        }
    }
}
