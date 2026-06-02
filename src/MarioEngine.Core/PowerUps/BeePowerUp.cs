using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class BeePowerUp : PowerUpBase
{
    public float FlySpeed { get; set; } = 200f;
    public float HoverDuration { get; set; } = 5f;

    public BeePowerUp()
    {
        Type = PowerUpType.Bee;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.CanFly = true;
        playerRef.FlySpeed = FlySpeed;
        playerRef.HoverDuration = HoverDuration;
    }
}
