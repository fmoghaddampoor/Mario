namespace MarioEngine.Core.Physics.Player;

using System.Numerics;

/// <summary>
/// Detects whether the player can grab onto a ledge.
/// Checks for a valid ledge within grab range using a solid-tile predicate.
/// </summary>
internal sealed class LedgeDetector
{
    /// <summary>Gets or sets the maximum distance the player can reach to grab a ledge.</summary>
    internal float GrabRange { get; set; } = 16f;

    /// <summary>
    /// Checks whether a valid ledge exists at the given player position and size.
    /// A ledge is valid when there is a solid surface to grab and an empty space above.
    /// </summary>
    /// <param name="playerPos">Center position of the player.</param>
    /// <param name="playerSize">Width and height of the player.</param>
    /// <param name="isSolid">Function that returns true if the given world position is solid.</param>
    /// <returns>True if a grabable ledge is detected.</returns>
    internal bool CanGrab(Vector2 playerPos, Vector2 playerSize, Func<Vector2, bool> isSolid)
    {
        var halfW = playerSize.X * 0.5f;
        var halfH = playerSize.Y * 0.5f;

        var grabY = playerPos.Y - halfH;
        var frontX = playerPos.X + halfW + GrabRange;

        var ledgePos = new Vector2(frontX, grabY);
        var abovePos = new Vector2(frontX, grabY - 4f);

        return isSolid(ledgePos) && !isSolid(abovePos);
    }
}
