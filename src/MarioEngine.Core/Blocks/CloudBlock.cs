using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class CloudBlock : BlockBase
{
    public bool IsSolidFromTopOnly { get; set; } = true;

    public CloudBlock()
    {
        IsSolid = true;
        IsBreakable = false;
    }
}
