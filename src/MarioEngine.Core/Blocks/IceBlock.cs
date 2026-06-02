using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class IceBlock : BlockBase
{
    public float FrictionMultiplier { get; set; } = 0.1f;

    public IceBlock()
    {
        IsSolid = true;
        IsBreakable = true;
    }
}
