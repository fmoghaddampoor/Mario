using System;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles dash attack movement.</summary>
public sealed class PlayerDashAttack
{
    /// <summary>Whether the player is currently dashing.</summary>
    public bool IsDashing { get; private set; }

    /// <summary>Speed of the dash.</summary>
    public float DashSpeed { get; set; } = 600f;

    /// <summary>Duration of the dash.</summary>
    public float DashDuration { get; set; } = 0.3f;

    private float _timer;

    /// <summary>Begins the dash attack.</summary>
    public void StartDash()
    {
        IsDashing = true;
        _timer = 0f;
    }

    /// <summary>Updates the dash timer; ends dash when duration elapses.</summary>
    public void Update(float dt)
    {
        if (!IsDashing) return;

        _timer += dt;
        if (_timer >= DashDuration)
        {
            IsDashing = false;
            _timer = 0f;
        }
    }
}
