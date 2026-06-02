using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class CatPowerUp : PowerUpBase
{
    public float ClimbSpeed { get; set; } = 200f;
    public float ScratchRange { get; set; } = 40f;

    public CatPowerUp()
    {
        Type = PowerUpType.Cat;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.CanClimbWalls = true;
        playerRef.ClimbSpeed = ClimbSpeed;
        playerRef.ScratchRange = ScratchRange;
    }
}
