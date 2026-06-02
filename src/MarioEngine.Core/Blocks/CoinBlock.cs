using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class CoinBlock : BlockBase
{
    public int CoinCount { get; set; } = 3;

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (CoinCount <= 0) return;
        CoinCount--;
    }
}
