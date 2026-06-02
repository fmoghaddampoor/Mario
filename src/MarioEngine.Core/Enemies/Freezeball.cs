using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Projectile that freezes the player on contact. Disappears after its lifetime expires.</summary>
internal sealed class Freezeball
{
    /// <summary>World position of the projectile.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Velocity in pixels per second.</summary>
    public Vector2 Velocity { get; set; }

    /// <summary>Maximum lifetime in seconds before the projectile vanishes.</summary>
    public float Lifetime { get; set; } = 5f;

    /// <summary>Whether the projectile has frozen something (or itself expired).</summary>
    public bool IsFrozen { get; private set; }

    private float _age;

    /// <summary>Updates position and age. Marks as frozen when expired.</summary>
    /// <param name="dt">Delta time.</param>
    public void Update(float dt)
    {
        if (IsFrozen) return;
        _age += dt;
        Position += Velocity * dt;
        if (_age >= Lifetime) IsFrozen = true;
    }
}
