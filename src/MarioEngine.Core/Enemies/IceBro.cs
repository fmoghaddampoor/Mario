using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Brother enemy that throws ice balls that freeze the player.</summary>
internal sealed class IceBro : EnemyBase
{
    /// <summary>Interval between ice ball throws in seconds.</summary>
    public float ThrowInterval { get; set; } = 2f;

    /// <summary>Speed of thrown ice balls.</summary>
    public float IceBallSpeed { get; set; } = 250f;

    private float _throwTimer;

    public IceBro()
    {
        Health = 3;
        Speed = 60f;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Fires an ice ball projectile toward the player direction.</summary>
    public void ThrowIceBall()
    {
        _throwTimer = 0;
        // Ice ball creation handled externally.
    }

    /// <summary>Throws ice balls on a timer.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        base.Update(dt);
        if (!IsAlive) return;
        _throwTimer += dt;
        if (_throwTimer >= ThrowInterval) ThrowIceBall();
    }
}
