using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Pipe plant that shoots fireballs during its emerge phase.</summary>
internal sealed class VenusFireTrap : PiranhaPlant
{
    /// <summary>Speed of fired fireballs.</summary>
    public float FireballSpeed { get; set; } = 300f;

    /// <summary>Delay between fireball shots in seconds.</summary>
    public float ShootInterval { get; set; } = 2.5f;

    private float _shootTimer;

    public VenusFireTrap()
    {
        Health = 2;
    }

    /// <summary>Fires a fireball projectile.</summary>
    public void ShootFireball()
    {
        // Fireball creation handled externally; this triggers the event.
        _shootTimer = 0;
    }

    /// <summary>Extends base update with fireball shooting logic.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        base.Update(dt);
        if (!IsEmerged || !IsAlive) return;

        _shootTimer += dt;
        if (_shootTimer >= ShootInterval) ShootFireball();
    }
}
