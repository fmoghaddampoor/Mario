namespace MarioEngine.Core.Physics.Zones;

using System.Numerics;

/// <summary>
/// A block that can be pushed by the player.
/// Tracks position, mass, and push state.
/// </summary>
internal sealed class PushableBlock
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PushableBlock"/> class.
    /// </summary>
    /// <param name="position">Initial position.</param>
    /// <param name="mass">Mass in kg.</param>
    internal PushableBlock(Vector2 position, float mass)
    {
        Position = position;
        Mass = mass;
        IsBeingPushed = false;
    }

    /// <summary>Gets or sets the current world position of the block.</summary>
    internal Vector2 Position { get; set; }

    /// <summary>Gets or sets the mass of the block in kg. Heavier blocks require more force to move.</summary>
    internal float Mass { get; set; }

    /// <summary>Gets or sets a value indicating whether the block is currently being pushed by the player.</summary>
    internal bool IsBeingPushed { get; set; }

    /// <summary>
    /// Applies a push force to the block, moving it in the given direction.
    /// </summary>
    /// <param name="direction">Normalized direction of the push.</param>
    /// <param name="strength">Magnitude of the push force.</param>
    internal void ApplyPush(Vector2 direction, float strength)
    {
        IsBeingPushed = true;
        Position += direction * (strength / Mass);
    }
}
