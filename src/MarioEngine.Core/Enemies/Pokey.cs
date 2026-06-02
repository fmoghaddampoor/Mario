using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Segmented cactus enemy. Loses segments as it takes damage, shrinking in size.</summary>
internal sealed class Pokey : EnemyBase
{
    /// <summary>All living body segments excluding the head.</summary>
    public List<PokeySegment> Segments { get; } = new();

    /// <summary>Segment spacing in pixels.</summary>
    private const float SegmentSpacing = 32f;

    public Pokey()
    {
        Health = Segments.Count + 1;
        Speed = 40f;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Damage the topmost segment. If it was the last, destroy the Pokey.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        if (!IsAlive) return;
        if (Segments.Count > 0)
        {
            var top = Segments[^1];
            top.TakeDamage(amount);
            if (!top.IsAlive) Segments.RemoveAt(Segments.Count - 1);
        }
        if (Segments.Count == 0) base.TakeDamage(amount);
        Health = Segments.Count + 1;
    }
}
