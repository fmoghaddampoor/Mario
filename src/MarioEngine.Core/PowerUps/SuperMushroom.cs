using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class SuperMushroom
{
    public Vector2 Position { get; set; }
    public float RiseSpeed { get; set; } = 50f;
    public float MoveSpeed { get; set; } = 80f;
    public bool IsRising { get; set; } = true;

    public void Spawn()
    {
        IsRising = true;
    }

    public void Update(float dt)
    {
        if (IsRising)
        {
            Position += new Vector2(0f, -RiseSpeed * dt);
        }
        else
        {
            Position += new Vector2(MoveSpeed * dt, 0f);
        }
    }
}
