using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class FireFlowerPowerUp : PowerUpBase
{
    public float FireballSpeed { get; set; } = 400f;
    public int MaxFireballs { get; set; } = 2;

    public FireFlowerPowerUp()
    {
        Type = PowerUpType.FireFlower;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.CanThrowFireballs = true;
        playerRef.FireballSpeed = FireballSpeed;
        playerRef.MaxFireballs = MaxFireballs;
    }
}
