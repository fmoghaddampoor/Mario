using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class SpikeBlock : BlockBase
{
    public int DamageAmount { get; set; } = 1;
    public bool HasTopSpike { get; set; }
    public bool HasBottomSpike { get; set; }
    public bool HasLeftSpike { get; set; }
    public bool HasRightSpike { get; set; }

    public SpikeBlock()
    {
        IsSolid = false;
    }
}
