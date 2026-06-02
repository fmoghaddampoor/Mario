using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class TimedBlock : BlockBase
{
    public float VisibleDuration { get; set; } = 2f;
    public float RespawnDuration { get; set; } = 3f;
    public bool IsVisible { get; set; } = true;

    public TimedBlock()
    {
        HitsRemaining = 1;
    }
}
