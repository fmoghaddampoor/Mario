using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles bubble blowing ability.</summary>
internal sealed class PlayerBubbleBlower
{
    /// <summary>Whether the player is currently blowing bubbles.</summary>
    public bool IsBlowing { get; set; }

    /// <summary>Speed at which bubbles travel.</summary>
    public float BubbleSpeed { get; set; } = 200f;

    /// <summary>Maximum number of active bubbles.</summary>
    public int MaxBubbles { get; set; } = 3;

    /// <summary>List of active bubble positions.</summary>
    public List<Vector2> ActiveBubbles { get; } = new();

    /// <summary>Blows a new bubble at the given position.</summary>
    public void BlowBubble(Vector2 position, bool facingRight)
    {
        if (ActiveBubbles.Count >= MaxBubbles) return;

        IsBlowing = true;
        ActiveBubbles.Add(position);
    }

    /// <summary>Updates all active bubble positions.</summary>
    public void UpdateBubbles(float dt)
    {
        for (var i = ActiveBubbles.Count - 1; i >= 0; i--)
        {
            var bubble = ActiveBubbles[i];
            bubble += new Vector2(BubbleSpeed * dt, 0);
            ActiveBubbles[i] = bubble;
        }

        IsBlowing = ActiveBubbles.Count > 0;
    }
}
