using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class LiquidBlock : BlockBase
{
    public bool IsLava { get; set; }
    public int DamagePerSecond { get; set; }
    public float DragMultiplier { get; set; } = 0.5f;

    public LiquidBlock()
    {
        IsSolid = false;
        IsBreakable = false;
        HitsRemaining = -1;

        if (!IsLava)
        {
            DamagePerSecond = 0;
            DragMultiplier = 0.5f;
        }
        else
        {
            DamagePerSecond = 1;
            DragMultiplier = 0f;
        }
    }
}
