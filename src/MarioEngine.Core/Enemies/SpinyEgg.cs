using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Egg dropped by Lakitu that falls and hatches into a Spiny.</summary>
internal sealed class SpinyEgg
{
    /// <summary>World position of the egg.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Fall speed in pixels per second.</summary>
    public float FallSpeed { get; set; } = 200f;

    /// <summary>Whether the egg is currently hatching.</summary>
    public bool Hatching { get; private set; }

    /// <summary>Time in seconds before the egg auto-hatches after landing.</summary>
    public float HatchTime { get; set; } = 1f;

    private float _timer;

    /// <summary>Updates fall physics and hatching timer.</summary>
    /// <param name="dt">Delta time.</param>
    public void Update(float dt)
    {
        if (Hatching)
        {
            _timer += dt;
            return;
        }
        Position += new Vector2(0, FallSpeed * dt);
    }

    /// <summary>Starts the hatching process. After HatchTime, spawns a Spiny.</summary>
    public void Hatch()
    {
        if (Hatching) return;
        Hatching = true;
        _timer = 0;
    }
}
