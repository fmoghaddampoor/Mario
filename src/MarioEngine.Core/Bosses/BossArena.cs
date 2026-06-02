using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossArena
{
    public float MinX { get; set; }
    public float MaxX { get; set; }
    public float MinY { get; set; }
    public float MaxY { get; set; }
    public List<Vector2> HazardPositions { get; set; } = new();

    public bool IsPlayerInArena(Vector2 playerPos)
    {
        return playerPos.X >= MinX && playerPos.X <= MaxX
            && playerPos.Y >= MinY && playerPos.Y <= MaxY;
    }

    public void Activate()
    {
    }

    public void Deactivate()
    {
    }
}
