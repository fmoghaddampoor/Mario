using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Floating sun that dive-bombs the player and returns to an anchor position.</summary>
internal sealed class AngrySun : EnemyBase
{
    /// <summary>Speed during dive attack.</summary>
    public float DiveSpeed { get; set; } = 500f;

    /// <summary>Speed when returning to anchor after a dive.</summary>
    public float ReturnSpeed { get; set; } = 100f;

    /// <summary>Whether the sun is currently diving.</summary>
    public bool IsDiving { get; private set; }

    /// <summary>Cooldown between dives in seconds.</summary>
    public float DiveCooldown { get; set; } = 3f;

    /// <summary>Home position the sun returns to between dives.</summary>
    public Vector2 AnchorPosition { get; set; }

    private float _cooldownTimer;

    public AngrySun()
    {
        Health = int.MaxValue; // Unkillable
        AnchorPosition = Position;
        StateMachine.TransitionTo(EnemyState.Idle);
    }

    /// <summary>Handles dive logic and return to anchor.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        if (IsDiving) return;

        _cooldownTimer += dt;
        if (_cooldownTimer >= DiveCooldown)
        {
            IsDiving = true;
            _cooldownTimer = 0;
            StateMachine.TransitionTo(EnemyState.Attack);
        }

        if (!IsDiving)
        {
            Vector2 toAnchor = AnchorPosition - Position;
            if (toAnchor.LengthSquared() > 1f)
                Velocity = Vector2.Normalize(toAnchor) * ReturnSpeed;
            else Velocity = Vector2.Zero;
        }

        base.Update(dt);
    }
}
