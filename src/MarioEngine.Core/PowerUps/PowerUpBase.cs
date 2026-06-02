using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public enum PowerUpType
{
    Mushroom,
    FireFlower,
    Star,
    Penguin,
    Bee,
    Boo,
    Propeller,
    Metal,
    Cat,
    Builder
}

public abstract class PowerUpBase
{
    public Vector2 Position { get; set; }
    public PowerUpType Type { get; protected set; }
    public float Duration { get; set; } = 0f;
    public bool IsExpired { get; protected set; }

    public virtual void OnCollect(Player playerRef)
    {
    }

    public virtual void OnExpire()
    {
        IsExpired = true;
    }
}
