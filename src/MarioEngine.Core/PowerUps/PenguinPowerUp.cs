using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class PenguinPowerUp : PowerUpBase
{
    public float SlideSpeedMultiplier { get; set; } = 2f;

    public PenguinPowerUp()
    {
        Type = PowerUpType.Penguin;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.CanIceSlide = true;
        playerRef.CanSwim = true;
        playerRef.SlideSpeedMultiplier = SlideSpeedMultiplier;
    }
}
