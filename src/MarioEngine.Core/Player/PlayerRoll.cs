using System;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles player roll dodge state and timing.</summary>
public sealed class PlayerRoll
{
    /// <summary>Whether the player is currently rolling.</summary>
    public bool IsRolling { get; private set; }

    /// <summary>Total duration of the roll.</summary>
    public float RollDuration { get; set; } = 0.3f;

    /// <summary>Elapsed time since roll started.</summary>
    public float RollTimer { get; private set; }

    /// <summary>Horizontal speed during the roll.</summary>
    public float RollSpeed { get; set; } = 400f;

    /// <summary>Whether the player is invincible during the roll.</summary>
    public bool IsInvincibleDuringRoll => IsRolling;

    /// <summary>Begins the roll.</summary>
    public void StartRoll()
    {
        IsRolling = true;
        RollTimer = 0f;
    }

    /// <summary>Updates the roll timer; ends roll when duration elapses.</summary>
    public void Update(float dt)
    {
        if (!IsRolling) return;

        RollTimer += dt;
        if (RollTimer >= RollDuration)
        {
            IsRolling = false;
            RollTimer = 0f;
        }
    }
}
