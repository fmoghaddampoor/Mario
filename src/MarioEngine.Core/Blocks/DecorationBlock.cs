using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class DecorationBlock : BlockBase
{
    public string DecorationType { get; set; } = string.Empty;

    public DecorationBlock()
    {
        IsSolid = false;
        IsBreakable = false;
        HitsRemaining = -1;
    }
}
