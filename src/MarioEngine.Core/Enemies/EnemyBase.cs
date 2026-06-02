using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Abstract base for all enemy types. Contains position, velocity, health, and a state machine.</summary>
internal abstract class EnemyBase
{
    private readonly EnemyStateMachine _stateMachine = new();

    /// <summary>World position of the enemy.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Current velocity in pixels per second.</summary>
    public Vector2 Velocity { get; set; }

    /// <summary>Current health points.</summary>
    public int Health { get; protected set; }

    /// <summary>Whether the enemy is still alive.</summary>
    public bool IsAlive { get; protected set; } = true;

    /// <summary>Base movement speed in pixels per second.</summary>
    public float Speed { get; set; }

    /// <summary>Gets the internal state machine for AI state transitions.</summary>
    public EnemyStateMachine StateMachine => _stateMachine;

    /// <summary>Called every frame to update position, velocity, and state.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public virtual void Update(float dt)
    {
        Position += Velocity * dt;
        _stateMachine.Update(dt);
    }

    /// <summary>Reduces health by <paramref name="amount"/>. Calls <see cref="Die"/> when health reaches zero.</summary>
    /// <param name="amount">Amount of damage to apply.</param>
    public virtual void TakeDamage(int amount)
    {
        if (!IsAlive) return;
        Health -= amount;
        if (Health <= 0) Die();
    }

    /// <summary>Marks the enemy as dead and transitions to the <see cref="EnemyState.Dead"/> state.</summary>
    public virtual void Die()
    {
        IsAlive = false;
        _stateMachine.TransitionTo(EnemyState.Dead);
    }
}
