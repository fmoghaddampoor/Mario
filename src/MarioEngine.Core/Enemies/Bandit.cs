using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Thief enemy that steals the player's held item on contact.</summary>
internal sealed class Bandit : EnemyBase
{
    /// <summary>Whether the bandit is currently carrying a stolen item.</summary>
    public bool HasStolenItem { get; private set; }

    public Bandit()
    {
        Health = 2;
        Speed = 90f;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Steals an item from the player. Marks the bandit as carrying an item.</summary>
    public void StealItem()
    {
        if (HasStolenItem) return;
        HasStolenItem = true;
        StateMachine.TransitionTo(EnemyState.Chase);
        // The bandit flees after stealing.
        Velocity = new Vector2(-MathF.Sign(Velocity.X) * Speed, Velocity.Y);
    }

    /// <summary>Drops the stolen item (e.g. on death or hit).</summary>
    public void DropItem()
    {
        HasStolenItem = false;
    }

    /// <summary>On death, drops any stolen item.</summary>
    public override void Die()
    {
        DropItem();
        base.Die();
    }
}
