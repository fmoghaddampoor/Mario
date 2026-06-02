namespace MarioEngine.Core.GamePlayer;

using System;
using System.Numerics;

/// <summary>Manages the player's cap, its position offset, and loss/retrieval state.</summary>
public sealed class PlayerCapPhysics
{
    /// <summary>Gets whether the player currently has their cap.</summary>
    public bool HasCap { get; private set; } = true;

    /// <summary>Gets or sets the positional offset of the cap relative to the player.</summary>
    public Vector2 CapOffset { get; set; } = new Vector2(0f, -40f);

    /// <summary>Calculates the world-space position of the cap based on player facing direction.</summary>
    /// <param name="playerPos">The player's current world position.</param>
    /// <param name="facingRight"><c>true</c> if the player is facing right.</param>
    /// <returns>The world-space cap position.</returns>
    public Vector2 GetCapPosition(Vector2 playerPos, bool facingRight)
    {
        float direction = facingRight ? 1f : -1f;
        return new Vector2(playerPos.X + CapOffset.X * direction, playerPos.Y + CapOffset.Y);
    }

    /// <summary>Marks the cap as lost.</summary>
    public void LoseCap()
    {
        HasCap = false;
    }

    /// <summary>Marks the cap as retrieved.</summary>
    public void RetrieveCap()
    {
        HasCap = true;
    }
}
