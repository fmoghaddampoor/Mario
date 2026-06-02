using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Basic patrol enemy. Walks back and forth, dies when stomped.</summary>
internal sealed class Goomba : EnemyBase
{
    private float _patrolOrigin;
    private bool _movingRight = true;

    /// <summary>Patrol movement speed in pixels per second.</summary>
    public float PatrolSpeed { get; set; } = 60f;

    /// <summary>Maximum distance from origin before turning around.</summary>
    public float PatrolRange { get; set; } = 100f;

    public Goomba()
    {
        Speed = PatrolSpeed;
        Health = 1;
        _patrolOrigin = Position.X;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Patrol behaviour: walk forward until reaching PatrolRange, then reverse.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;

        float offset = Position.X - _patrolOrigin;
        if (offset > PatrolRange) _movingRight = false;
        else if (offset < -PatrolRange) _movingRight = true;

        Velocity = new Vector2(_movingRight ? PatrolSpeed : -PatrolSpeed, 0);
        base.Update(dt);
    }

    /// <summary>Stomping from above kills the Goomba and triggers squish animation.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        StateMachine.TransitionTo(EnemyState.Hit);
        base.TakeDamage(amount);
    }

    /// <summary>Death triggers squish (handled by animation system).</summary>
    public override void Die()
    {
        StateMachine.TransitionTo(EnemyState.Dying);
        base.Die();
    }
}
