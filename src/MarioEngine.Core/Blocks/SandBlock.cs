using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class SandBlock : BlockBase
{
    public float CrumbleDelay { get; set; } = 0.5f;
    public bool IsCrumblng { get; private set; }

    public SandBlock()
    {
        IsBreakable = true;
    }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        IsCrumblng = true;
    }
}
