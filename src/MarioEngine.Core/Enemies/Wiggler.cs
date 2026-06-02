using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Caterpillar enemy. Calm at full HP, angry when damaged (moves faster, changes colour).</summary>
internal sealed class Wiggler : EnemyBase
{
    /// <summary>Whether the Wiggler is enraged from taking damage.</summary>
    public bool IsAngry { get; private set; }

    /// <summary>Speed multiplier applied when angry.</summary>
    public float AngrySpeedMultiplier { get; set; } = 2f;

    private float _baseSpeed;

    public Wiggler()
    {
        Health = 3;
        _baseSpeed = 60f;
        Speed = _baseSpeed;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Toggles angry state when health drops below max.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (Health < 3 && !IsAngry)
        {
            IsAngry = true;
            Speed = _baseSpeed * AngrySpeedMultiplier;
            StateMachine.TransitionTo(EnemyState.Chase);
        }
    }
}
