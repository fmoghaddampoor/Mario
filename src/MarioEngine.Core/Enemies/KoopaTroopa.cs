using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Koopa that walks until hit, then retreats into a shell that can be kicked.</summary>
internal sealed class KoopaTroopa : EnemyBase
{
    /// <summary>Whether the Koopa is currently in shell mode.</summary>
    public bool InShell { get; private set; }

    /// <summary>Speed of the shell when kicked.</summary>
    public float ShellSpeed { get; set; } = 400f;

    public KoopaTroopa()
    {
        Health = 2;
        Speed = 80f;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Kicks the shell in the given direction.</summary>
    /// <param name="direction">Normalized direction vector.</param>
    public void KickShell(Vector2 direction)
    {
        if (!InShell) return;
        Velocity = direction * ShellSpeed;
        StateMachine.TransitionTo(EnemyState.Chase);
    }

    /// <summary>Pops the Koopa out of its shell, restoring walking state.</summary>
    public void PopOutOfShell()
    {
        InShell = false;
        Health = 2;
        Speed = 80f;
        Velocity = Vector2.Zero;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Hit forces the Koopa into its shell.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        if (InShell) return;
        InShell = true;
        Health = 1;
        Speed = 0;
        Velocity = Vector2.Zero;
        StateMachine.TransitionTo(EnemyState.Attack);
    }

    /// <summary>Update movement when not in shell.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!InShell) base.Update(dt);
        else Position += Velocity * dt;
    }
}
