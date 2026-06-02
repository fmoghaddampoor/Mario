using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class StarPowerUp : PowerUpBase
{
    public float SpeedMultiplier { get; set; } = 1.5f;

    public StarPowerUp()
    {
        Type = PowerUpType.Star;
        Duration = 10f;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.IsInvincible = true;
        playerRef.MoveSpeed *= SpeedMultiplier;
    }

    public override void OnExpire()
    {
        base.OnExpire();
    }
}
