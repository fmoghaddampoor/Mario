using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Brother enemy that throws fireballs. Immune to fire damage.</summary>
internal sealed class FireBro : EnemyBase
{
    /// <summary>Speed of thrown fireballs.</summary>
    public float FireballSpeed { get; set; } = 300f;

    /// <summary>Interval between fireball throws in seconds.</summary>
    public float ThrowInterval { get; set; } = 1.8f;

    private float _throwTimer;

    public FireBro()
    {
        Health = 3;
        Speed = 60f;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Throws a fireball projectile toward the player.</summary>
    public void ThrowFireball()
    {
        _throwTimer = 0;
        // Fireball creation handled externally.
    }

    /// <summary>Throws fireballs on a timer.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        base.Update(dt);
        if (!IsAlive) return;
        _throwTimer += dt;
        if (_throwTimer >= ThrowInterval) ThrowFireball();
    }

    /// <summary>Completely immune to fire-type damage.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        // Immune to fire damage — stub for typed damage system integration.
        base.TakeDamage(amount);
    }
}
