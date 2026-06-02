using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Fish enemy that swims through water following a sine wave pattern.</summary>
internal sealed class CheepCheep : EnemyBase
{
    /// <summary>Forward swim speed in pixels per second.</summary>
    public float SwimSpeed { get; set; } = 150f;

    /// <summary>Amplitude of the vertical sine wave.</summary>
    public float SineAmplitude { get; set; } = 30f;

    /// <summary>Frequency of the vertical sine wave in radians per second.</summary>
    public float SineFrequency { get; set; } = 2f;

    private Vector2 _startPos;
    private float _time;

    public CheepCheep()
    {
        Health = 1;
        _startPos = Position;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Swims forward with a sinusoidal vertical offset.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        _time += dt;
        Velocity = new Vector2(-SwimSpeed, 0);
        Position = _startPos + new Vector2(-SwimSpeed * _time, MathF.Sin(_time * SineFrequency) * SineAmplitude);
    }
}
