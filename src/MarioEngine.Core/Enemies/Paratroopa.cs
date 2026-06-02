using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Winged Koopa that flies in a sinusoidal pattern.</summary>
internal sealed class Paratroopa : EnemyBase
{
    /// <summary>Vertical oscillation amplitude in pixels.</summary>
    public float FlightAmplitude { get; set; } = 40f;

    /// <summary>Sinusoidal frequency in radians per second.</summary>
    public float FlightFrequency { get; set; } = 2f;

    private Vector2 _anchor;
    private float _time;

    public Paratroopa()
    {
        Health = 2;
        Speed = 60f;
        _anchor = Position;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Sinusoidal flight motion on top of patrol.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        _time += dt;
        float sin = MathF.Sin(_time * FlightFrequency);
        Position = _anchor + new Vector2(0, sin * FlightAmplitude);
        // Horizontal patrol
        Velocity = new Vector2(Speed * (sin > 0 ? 1 : -1), 0);
        base.Update(dt);
        _anchor = new Vector2(Position.X, _anchor.Y);
    }
}
