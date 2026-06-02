using System;

namespace MarioEngine.Core.Player;

/// <summary>Handles invincibility roll state and timing.</summary>
internal sealed class PlayerInvincibilityRoll
{
    /// <summary>Whether the player is currently rolling.</summary>
    public bool IsRolling { get; private set; }

    /// <summary>Total duration of the invincibility roll.</summary>
    public float RollDuration { get; set; } = 0.4f;

    /// <summary>Whether the player is invincible during the roll.</summary>
    public bool IsInvincible { get; private set; }

    private float _timer;

    /// <summary>Begins the invincibility roll.</summary>
    public void StartRoll()
    {
        IsRolling = true;
        IsInvincible = true;
        _timer = 0f;
    }

    /// <summary>Updates the roll timer; ends roll when duration elapses.</summary>
    public void Update(float dt)
    {
        if (!IsRolling) return;

        _timer += dt;
        if (_timer >= RollDuration)
        {
            IsRolling = false;
            IsInvincible = false;
            _timer = 0f;
        }
    }
}
