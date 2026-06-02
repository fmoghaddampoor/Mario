using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Spiny that walks on ceilings with spikes on top. Immune to stomp attacks.</summary>
internal sealed class Spiny : EnemyBase
{
    /// <summary>Whether the Spiny is walking on a ceiling.</summary>
    public bool OnCeiling { get; set; }

    public Spiny()
    {
        Health = 1;
        Speed = 80f;
        OnCeiling = true;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Override: Spiny is immune to stomp-type damage (amount 1).</summary>
    /// <param name="amount">Damage amount. Ignored if 1 (stomp).</param>
    public override void TakeDamage(int amount)
    {
        if (amount <= 1) return;
        base.TakeDamage(amount);
    }
}
