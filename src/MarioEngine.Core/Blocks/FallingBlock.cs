using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class FallingBlock : BlockBase
{
    public float FallDelay { get; set; } = 0.3f;
    public float RespawnTime { get; set; } = 3f;
    public bool IsFalling { get; private set; }
    public bool IsRespawning { get; private set; }

    public FallingBlock()
    {
        IsSolid = true;
        IsBreakable = false;
    }
}
