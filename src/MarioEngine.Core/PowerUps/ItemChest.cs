using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class ItemChest
{
    public Vector2 Position { get; set; }
    public PowerUpType? Contents { get; set; }
    public bool Opened { get; private set; }

    public void Open()
    {
        if (Opened) return;
        Opened = true;
    }
}
