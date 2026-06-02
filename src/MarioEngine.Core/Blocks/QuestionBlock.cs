using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class QuestionBlock : BlockBase
{
    public PowerUpType Contents { get; set; }
    public bool Used { get; private set; }

    public QuestionBlock()
    {
        HitsRemaining = 1;
    }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (Used) return;
        Used = true;
    }
}
