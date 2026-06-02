using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Giant bullet bill that locks onto a target position and fires in a straight line.</summary>
internal sealed class BanzaiBill : EnemyBase
{
    /// <summary>Speed when launched toward the target.</summary>
    public float LaunchSpeed { get; set; } = 600f;

    /// <summary>Whether this Banzai Bill has been launched.</summary>
    public bool IsLaunched { get; private set; }

    /// <summary>Locks onto a target position and fires in a straight line.</summary>
    /// <param name="targetPosition">World position of the target (typically the player).</param>
    public void Launch(Vector2 targetPosition)
    {
        if (IsLaunched) return;
        IsLaunched = true;
        Vector2 dir = Vector2.Normalize(targetPosition - Position);
        Velocity = dir * LaunchSpeed;
        StateMachine.TransitionTo(EnemyState.Attack);
    }

    /// <summary>Moves in a straight line after launch.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsLaunched) return;
        Position += Velocity * dt;
    }
}
