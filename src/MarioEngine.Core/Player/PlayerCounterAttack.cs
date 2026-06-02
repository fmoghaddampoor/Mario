using System;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles counter attack timing window.</summary>
public sealed class PlayerCounterAttack
{
    /// <summary>Whether the player is currently countering.</summary>
    public bool IsCountering { get; private set; }

    /// <summary>Time window in seconds during which a counter can succeed.</summary>
    public float CounterWindow { get; set; } = 0.15f;

    /// <summary>Duration of the counter animation.</summary>
    public float CounterDuration { get; set; } = 0.3f;

    private float _timer;
    private float _counterTimer;

    /// <summary>Attempts to counter; returns true if within the counter window.</summary>
    public bool TryCounter()
    {
        if (_timer > 0f && _timer <= CounterWindow)
        {
            IsCountering = true;
            _counterTimer = 0f;
            return true;
        }

        return false;
    }

    /// <summary>Updates internal timers.</summary>
    public void Update(float dt)
    {
        if (_timer > 0f)
            _timer -= dt;

        if (!IsCountering) return;

        _counterTimer += dt;
        if (_counterTimer >= CounterDuration)
        {
            IsCountering = false;
            _counterTimer = 0f;
        }
    }

    /// <summary>Starts the counter window externally.</summary>
    public void StartCounterWindow()
    {
        _timer = CounterWindow;
    }
}
