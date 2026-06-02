using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Pipe-dwelling plant that emerges and retreats on a fixed cycle.</summary>
internal class PiranhaPlant : EnemyBase
{
    /// <summary>Height the plant extends above the pipe.</summary>
    public float EmergeHeight { get; set; } = 64f;

    /// <summary>Full emerge-retreat cycle duration in seconds.</summary>
    public float CycleDuration { get; set; } = 2f;

    /// <summary>Current timer within the cycle.</summary>
    public float Timer { get; protected set; }

    /// <summary>Whether the plant is currently emerged.</summary>
    public bool IsEmerged { get; protected set; }

    private float _emergeLerp;
    private Vector2 _pipeBase;

    public PiranhaPlant()
    {
        Health = 1;
        Speed = 0;
        _pipeBase = Position;
        StateMachine.TransitionTo(EnemyState.Attack);
    }

    /// <summary>Cycles emerge and retreat based on timer.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        Timer += dt;

        float halfCycle = CycleDuration * 0.5f;
        if (Timer <= halfCycle)
        {
            _emergeLerp = Timer / halfCycle;
            IsEmerged = true;
        }
        else if (Timer <= CycleDuration)
        {
            _emergeLerp = 1f - (Timer - halfCycle) / halfCycle;
            IsEmerged = _emergeLerp > 0.01f;
        }
        else
        {
            Timer = 0;
            IsEmerged = false;
        }

        Position = _pipeBase + new Vector2(0, -_emergeLerp * EmergeHeight);
    }
}
