using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Aquatic squid enemy that swims in a sine pattern and can spit ink.</summary>
internal sealed class Blooper : EnemyBase
{
    /// <summary>Duration of the ink cloud effect in seconds.</summary>
    public float InkDuration { get; set; } = 3f;

    private float _swimTime;
    private Vector2 _startPos;

    public Blooper()
    {
        Health = 2;
        Speed = 120f;
        _startPos = Position;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Sine-based swim movement.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        _swimTime += dt;
        float wave = MathF.Sin(_swimTime * 2f) * 20f;
        Position = _startPos + new Vector2(0, wave) + new Vector2(-Speed * _swimTime, 0);
        base.Update(dt);
    }

    /// <summary>Spits ink, creating a cloud that obscures vision for InkDuration.</summary>
    public void SpitInk()
    {
        // Ink cloud creation handled externally.
    }

    /// <summary>Returns the world position where ink should spawn.</summary>
    public Vector2 GetInkPosition() => Position + new Vector2(-20, 0);
}
