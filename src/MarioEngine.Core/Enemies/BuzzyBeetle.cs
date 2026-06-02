using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Beetle with a hardened shell. Cannot be stomped from above while in shell mode.</summary>
internal sealed class BuzzyBeetle : EnemyBase
{
    /// <summary>Whether the shell is hardened (immune to stomp).</summary>
    public bool ShellHardened { get; set; }

    public BuzzyBeetle()
    {
        Health = 2;
        Speed = 50f;
        ShellHardened = true;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>When shell is hardened, only damage from the sides or items applies.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        if (ShellHardened && amount <= 1) return;
        base.TakeDamage(amount);
    }
}
