using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Winged Goomba. Loses its wings when stomped and becomes a normal Goomba.</summary>
internal sealed class Paragoomba : EnemyBase
{
    /// <summary>Whether this Paragoomba still has its wings.</summary>
    public bool HasWings { get; private set; } = true;

    private float _patrolOrigin;
    private bool _movingRight = true;

    public Paragoomba()
    {
        Health = 2;
        Speed = 60f;
        _patrolOrigin = Position.X;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Patrol with hop when winged.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        float offset = Position.X - _patrolOrigin;
        if (offset > 100f) _movingRight = false;
        else if (offset < -100f) _movingRight = true;
        Velocity = new Vector2(_movingRight ? Speed : -Speed, 0);
        base.Update(dt);
    }

    /// <summary>First hit removes wings. Second hit kills.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        if (HasWings)
        {
            HasWings = false;
            Health = 1;
            Speed = 60f;
            StateMachine.TransitionTo(EnemyState.Hit);
            return;
        }
        base.TakeDamage(amount);
    }
}
