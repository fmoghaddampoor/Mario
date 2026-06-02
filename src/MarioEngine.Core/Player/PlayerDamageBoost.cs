using System;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles temporary speed boost after taking damage.</summary>
public sealed class PlayerDamageBoost
{
    /// <summary>Whether the damage boost is active.</summary>
    public bool IsBoosting { get; private set; }

    /// <summary>Speed multiplier applied during the boost.</summary>
    public float BoostSpeedMultiplier { get; set; } = 1.5f;

    /// <summary>Duration of the boost in seconds.</summary>
    public float BoostDuration { get; set; } = 0.5f;

    private float _timer;

    /// <summary>Begins the damage boost.</summary>
    public void StartBoost()
    {
        IsBoosting = true;
        _timer = 0f;
    }

    /// <summary>Updates the boost timer; ends boost when duration elapses.</summary>
    public void Update(float dt)
    {
        if (!IsBoosting) return;

        _timer += dt;
        if (_timer >= BoostDuration)
        {
            IsBoosting = false;
            _timer = 0f;
        }
    }
}
