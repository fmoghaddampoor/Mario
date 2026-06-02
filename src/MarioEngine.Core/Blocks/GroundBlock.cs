using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class GroundBlock : BlockBase
{
    public GroundBlock()
    {
        IsSolid = true;
        IsBreakable = false;
        HitsRemaining = -1;
    }
}
