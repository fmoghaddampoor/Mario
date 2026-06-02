using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class ConveyorBlock : BlockBase
{
    public Vector2 ConveyorDirection { get; set; }
    public float ConveyorSpeed { get; set; } = 100f;

    public Vector2 GetConveyorForce()
    {
        return ConveyorDirection * ConveyorSpeed;
    }
}
