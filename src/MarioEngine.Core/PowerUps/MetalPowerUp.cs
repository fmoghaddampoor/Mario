using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class MetalPowerUp : PowerUpBase
{
    public float WeightMultiplier { get; set; } = 3f;

    public MetalPowerUp()
    {
        Type = PowerUpType.Metal;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.WeightMultiplier = WeightMultiplier;
        playerRef.CanCrushBlocks = true;
        playerRef.SinksInWater = true;
    }
}
