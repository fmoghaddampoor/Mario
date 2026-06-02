using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class Fireball
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Lifetime { get; set; } = 2f;
    public bool IsActive { get; set; } = true;

    public void Update(float dt)
    {
        if (!IsActive) return;

        Position += Velocity * dt;
        Lifetime -= dt;

        if (Lifetime <= 0f)
        {
            IsActive = false;
        }
    }

    public bool IsExpired => Lifetime <= 0f || !IsActive;
}
