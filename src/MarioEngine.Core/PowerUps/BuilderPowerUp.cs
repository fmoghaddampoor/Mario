using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class BuilderPowerUp : PowerUpBase
{
    public float PlaceRange { get; set; } = 100f;
    public int MaxBlocks { get; set; } = 3;

    public BuilderPowerUp()
    {
        Type = PowerUpType.Builder;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.CanPlaceBlocks = true;
        playerRef.CanBreakBlocks = true;
        playerRef.PlaceRange = PlaceRange;
        playerRef.MaxPlaceableBlocks = MaxBlocks;
    }
}
