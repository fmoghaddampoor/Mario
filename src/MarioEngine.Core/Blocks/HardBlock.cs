using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class HardBlock : BlockBase
{
    public HardBlock()
    {
        IsSolid = true;
        IsBreakable = false;
        HitsRemaining = -1;
    }
}
