using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>A standalone shell physics object that slides and bounces off walls.</summary>
internal sealed class KoopaShell
{
    /// <summary>Current speed magnitude.</summary>
    public float Speed { get; set; }

    /// <summary>Normalized movement direction.</summary>
    public Vector2 Direction { get; set; }

    /// <summary>Whether the shell is currently moving.</summary>
    public bool Moving { get; private set; }

    /// <summary>Whether the shell bounced off a wall this frame.</summary>
    public bool Bouncing { get; private set; }

    private Vector2 _position;

    /// <summary>Launches the shell with the given direction and speed.</summary>
    /// <param name="dir">Movement direction (will be normalized).</param>
    /// <param name="speed">Launch speed in pixels per second.</param>
    public void Launch(Vector2 dir, float speed)
    {
        Direction = Vector2.Normalize(dir);
        Speed = speed;
        Moving = true;
        Bouncing = false;
    }

    /// <summary>Updates shell position each frame.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        if (!Moving) return;
        _position += Direction * Speed * dt;
        Bouncing = false;
    }

    /// <summary>Called on wall collision. Reverses the appropriate axis.</summary>
    public void BounceOffWall()
    {
        Direction = new Vector2(-Direction.X, Direction.Y);
        Bouncing = true;
    }
}
