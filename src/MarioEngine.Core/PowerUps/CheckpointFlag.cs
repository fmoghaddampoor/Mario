using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class CheckpointFlag
{
    public Vector2 Position { get; set; }
    public bool Activated { get; private set; }

    public void Activate()
    {
        Activated = true;
    }

    public void Deactivate()
    {
        Activated = false;
    }
}
