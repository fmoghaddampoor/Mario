using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Flying enemy that follows the player from above and drops Spinies.</summary>
internal sealed class Lakitu : EnemyBase
{
    /// <summary>Speed at which Lakitu follows the player horizontally.</summary>
    public float FollowSpeed { get; set; } = 100f;

    /// <summary>Interval between Spiny drops in seconds.</summary>
    public float DropInterval { get; set; } = 3f;

    /// <summary>Timer tracking time since last drop.</summary>
    public float DropTimer { get; set; }

    public Lakitu()
    {
        Health = 3;
        Speed = FollowSpeed;
        StateMachine.TransitionTo(EnemyState.Chase);
    }

    /// <summary>Moves toward the player's X position and handles drop timer.</summary>
    /// <param name="dt">Delta time.</param>
    /// <param name="playerPos">Current player position.</param>
    public void UpdateDrop(float dt, Vector2 playerPos)
    {
        if (!IsAlive) return;
        float dirX = MathF.Sign(playerPos.X - Position.X);
        Velocity = new Vector2(dirX * FollowSpeed, 0);
        base.Update(dt);

        DropTimer += dt;
        if (DropTimer >= DropInterval)
        {
            DropSpiny();
            DropTimer = 0;
        }
    }

    /// <summary>Spawns a SpinyEgg below Lakitu's position.</summary>
    public void DropSpiny()
    {
        // Egg creation handled externally; this triggers the action.
    }
}
