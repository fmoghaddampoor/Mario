using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class CheckpointBlock : BlockBase
{
    public bool IsActivated { get; private set; }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        IsActivated = true;
    }
}
