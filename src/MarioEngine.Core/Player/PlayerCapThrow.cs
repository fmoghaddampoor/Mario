using System;
using System.Numerics;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles cap throwing ability.</summary>
public sealed class PlayerCapThrow
{
    /// <summary>Whether the cap is currently thrown.</summary>
    public bool IsThrowing { get; private set; }

    /// <summary>Current position of the thrown cap.</summary>
    public Vector2 CapPosition { get; private set; }

    /// <summary>Speed at which the cap travels.</summary>
    public float ThrowSpeed { get; set; } = 500f;

    /// <summary>Duration the cap stays out before returning.</summary>
    public float ThrowDuration { get; set; } = 0.5f;

    private float _timer;
    private int _direction;

    /// <summary>Fired when the cap returns to the player.</summary>
    public event Action? OnCapReturned;

    /// <summary>Throws the cap in the facing direction.</summary>
    public void Throw(bool facingRight)
    {
        IsThrowing = true;
        _timer = 0f;
        _direction = facingRight ? 1 : -1;
    }

    /// <summary>Updates cap position and checks return.</summary>
    public void Update(float dt)
    {
        if (!IsThrowing) return;

        _timer += dt;
        CapPosition += new Vector2(ThrowSpeed * _direction * dt, 0);

        if (_timer >= ThrowDuration)
        {
            IsThrowing = false;
            OnCapReturned?.Invoke();
        }
    }
}
