using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class StarCoin
{
    public Vector2 Position { get; set; }
    public int Index { get; set; }
    public bool Collected { get; private set; }

    public StarCoin(int index)
    {
        Index = index;
    }

    public void OnCollect()
    {
        Collected = true;
    }
}
