using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class MysteryBlock : BlockBase
{
    public List<PowerUpType> PossibleContents { get; set; } = new();

    public MysteryBlock()
    {
        HitsRemaining = 1;
    }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (HitsRemaining <= 0) return;
        HitsRemaining--;
    }
}
