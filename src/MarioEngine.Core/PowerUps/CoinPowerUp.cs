using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class CoinPowerUp
{
    public Vector2 Position { get; set; }
    public int CoinValue { get; set; } = 1;
    public bool Collected { get; private set; }
    public float RiseAnimation { get; set; }

    public void OnCollect()
    {
        Collected = true;
        RiseAnimation = 0.5f;
    }
}
