using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class BreakableWall : BlockBase
{
    public PowerUpType RequiredPowerUp { get; set; }

    public BreakableWall()
    {
        IsBreakable = true;
    }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (player.CurrentPowerUpType == RequiredPowerUp)
        {
            OnBreak();
        }
    }

    public override void OnBreak()
    {
        base.OnBreak();
    }
}
