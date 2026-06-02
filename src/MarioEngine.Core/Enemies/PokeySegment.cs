using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>A single body segment of a <see cref="Pokey"/> enemy. Positioned relative to the parent.</summary>
internal sealed class PokeySegment
{
    /// <summary>Offset relative to the parent Pokey's position.</summary>
    public Vector2 Offset { get; set; }

    /// <summary>Health of this segment. Defaults to 1.</summary>
    public int Health { get; set; } = 1;

    /// <summary>Whether this segment is still alive.</summary>
    public bool IsAlive { get; private set; } = true;

    /// <summary>Reduces segment health by <paramref name="amount"/>. Marks as dead when health reaches zero.</summary>
    /// <param name="amount">Damage to apply.</param>
    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;
        Health -= amount;
        if (Health <= 0) IsAlive = false;
    }
}
