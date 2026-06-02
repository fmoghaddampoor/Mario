using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class PowerUpBlock
{
    public Vector2 Position { get; set; }
    public PowerUpType Contents { get; set; }
    public bool Used { get; private set; }

    public void Activate()
    {
        if (Used) return;
        Used = true;
        SpawnContents();
    }

    public void SpawnContents()
    {
    }
}
