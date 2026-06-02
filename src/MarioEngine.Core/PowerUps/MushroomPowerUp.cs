using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class MushroomPowerUp : PowerUpBase
{
    public float ScaleMultiplier { get; set; } = 1.5f;

    public MushroomPowerUp()
    {
        Type = PowerUpType.Mushroom;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.Scale *= ScaleMultiplier;
        playerRef.ExtraHitPoints += 1;
    }
}
