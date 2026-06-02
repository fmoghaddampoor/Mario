using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class BlockDebris
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Lifetime { get; set; } = 0.5f;

    public void Update(float dt)
    {
        Position += Velocity * dt;
        Velocity += new Vector2(0f, 400f) * dt;
        Lifetime -= dt;
    }

    public bool IsExpired => Lifetime <= 0f;
}
