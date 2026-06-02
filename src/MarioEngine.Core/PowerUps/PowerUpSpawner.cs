using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class PowerUpSpawner
{
    public Vector2 Position { get; set; }
    public PowerUpType Type { get; set; }
    public bool Spawned { get; private set; }

    public void Spawn()
    {
        Spawned = true;
    }

    public void Despawn()
    {
        Spawned = false;
    }
}
