using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Penguin that walks normally or belly slides at high speed.</summary>
internal sealed class Penguin : EnemyBase
{
    /// <summary>Walking speed in pixels per second.</summary>
    public float WalkSpeed { get; set; } = 80f;

    /// <summary>Belly slide speed in pixels per second.</summary>
    public float SlideSpeed { get; set; } = 300f;

    /// <summary>Whether the penguin is currently sliding on its belly.</summary>
    public bool IsSliding { get; private set; }

    public Penguin()
    {
        Health = 2;
        Speed = WalkSpeed;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Patrol walking or sliding depending on state.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        Speed = IsSliding ? SlideSpeed : WalkSpeed;
        Velocity = new Vector2(Speed * MathF.Sign(Velocity.X) is 0 ? -1 : MathF.Sign(Velocity.X), 0);
        base.Update(dt);
    }

    /// <summary>Start sliding.</summary>
    public void StartSlide() { IsSliding = true; StateMachine.TransitionTo(EnemyState.Chase); }
}
