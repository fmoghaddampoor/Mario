using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class PipeBlock : BlockBase
{
    public string TargetScene { get; set; } = string.Empty;
    public Vector2 ExitPosition { get; set; }

    public PipeBlock()
    {
        IsSolid = true;
    }

    public bool IsEnterable(Vector2 playerPos)
    {
        return false;
    }

    public void OnEnter()
    {
    }
}
