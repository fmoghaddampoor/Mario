using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class BrickBlock : BlockBase
{
    public bool IsBigPlayerOnly { get; set; } = true;

    public BrickBlock()
    {
        IsBreakable = true;
        HitsRemaining = 1;
    }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (player.IsBig)
        {
            OnBreak();
        }
    }

    public override void OnBreak()
    {
        base.OnBreak();
    }
}
