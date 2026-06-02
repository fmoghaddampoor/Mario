using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Unkillable lava-dwelling enemy that periodically leaps out of the lava.</summary>
internal sealed class LavaBubble : EnemyBase
{
    /// <summary>Peak height of each lava jump in pixels.</summary>
    public float JumpHeight { get; set; } = 100f;

    /// <summary>Time between jumps in seconds.</summary>
    public float JumpInterval { get; set; } = 1.5f;

    /// <summary>Whether the bubble is currently submerged in lava.</summary>
    public bool InLava { get; set; } = true;

    private float _jumpTimer;
    private float _jumpVelocity;

    public LavaBubble()
    {
        Health = int.MaxValue; // Unkillable
        StateMachine.TransitionTo(EnemyState.Idle);
    }

    /// <summary>Jump arc logic: launches upward, falls back into lava.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (InLava)
        {
            _jumpTimer += dt;
            if (_jumpTimer >= JumpInterval)
            {
                InLava = false;
                _jumpVelocity = MathF.Sqrt(2 * MathF.Abs(PhysicsGravity) * JumpHeight);
                _jumpTimer = 0;
                StateMachine.TransitionTo(EnemyState.Attack);
            }
            return;
        }

        _jumpVelocity += PhysicsGravity * dt;
        Position += new Vector2(0, _jumpVelocity * dt);

        if (Position.Y >= 0)
        {
            Position = new Vector2(Position.X, 0);
            InLava = true;
            _jumpVelocity = 0;
            StateMachine.TransitionTo(EnemyState.Idle);
        }
    }

    /// <summary>Gravity constant used for jump physics.</summary>
    private static float PhysicsGravity => -980f;
}
