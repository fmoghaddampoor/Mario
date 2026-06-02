using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class BooPowerUp : PowerUpBase
{
    public bool CanPhaseThrough { get; set; } = true;

    public BooPowerUp()
    {
        Type = PowerUpType.Boo;
        Duration = 8f;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.IsGhostForm = true;
        playerRef.CanPhaseThroughEnemies = CanPhaseThrough;
    }
}
