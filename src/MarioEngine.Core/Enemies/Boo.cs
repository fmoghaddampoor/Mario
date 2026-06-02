using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Classic Boo AI: hides when the player looks at it, chases when the player looks away.</summary>
internal sealed class Boo : EnemyBase
{
    /// <summary>Whether the Boo is currently chasing the player.</summary>
    public bool IsChasing { get; private set; }

    /// <summary>Chase speed in pixels per second.</summary>
    public float ChaseSpeed { get; set; } = 120f;

    /// <summary>Maximum distance at which the Boo can detect and react to the player.</summary>
    public float DetectionRange { get; set; } = 300f;

    public Boo()
    {
        Health = 1;
        StateMachine.TransitionTo(EnemyState.Idle);
    }

    /// <summary>Updates Boo behaviour based on player look direction and position.</summary>
    /// <param name="playerPos">Current player world position.</param>
    /// <param name="playerLookingAtBoo">Whether the player's facing direction is toward this Boo.</param>
    public void UpdateAI(Vector2 playerPos, bool playerLookingAtBoo)
    {
        if (!IsAlive) return;
        float dist = Vector2.Distance(Position, playerPos);
        if (dist > DetectionRange) { IsChasing = false; return; }

        if (playerLookingAtBoo)
        {
            IsChasing = false;
            // Face away from player
            StateMachine.TransitionTo(EnemyState.Idle);
        }
        else
        {
            IsChasing = true;
            Vector2 dir = Vector2.Normalize(playerPos - Position);
            Velocity = dir * ChaseSpeed;
            StateMachine.TransitionTo(EnemyState.Chase);
        }
    }

    /// <summary>Movement is handled by UpdateAI; no-op base update to avoid double movement.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (IsChasing) Position += Velocity * dt;
    }
}
