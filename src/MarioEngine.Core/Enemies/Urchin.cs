using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Stationary spiky water enemy. Damages the player on contact from any side.</summary>
internal sealed class Urchin
{
    /// <summary>World position.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Diameter of the Urchin in pixels. Used for collision.</summary>
    public float Size { get; set; } = 32f;

    /// <summary>Constructs an Urchin at the given position.</summary>
    /// <param name="position">Initial world position.</param>
    public Urchin(Vector2 position)
    {
        Position = position;
    }

    /// <summary>Returns the bounding radius for collision checks.</summary>
    public float Radius => Size * 0.5f;
}
