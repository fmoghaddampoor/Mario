namespace MarioEngine.Core.Physics.Zones;

using System.Numerics;

/// <summary>
/// Applies ice-like friction reduction to a body moving on an icy surface.
/// Greatly reduces deceleration so the body slides.
/// </summary>
internal sealed class IcePhysics
{
    /// <summary>Gets or sets the multiplier applied to normal friction (0.0 = full ice, 1.0 = no effect).</summary>
    internal float FrictionReduction { get; set; } = 0.1f;

    /// <summary>
    /// Applies ice friction to the given velocity, reducing deceleration.
    /// </summary>
    /// <param name="velocity">Current velocity of the body.</param>
    /// <param name="dt">Delta time in seconds.</param>
    /// <returns>Velocity after ice friction is applied.</returns>
    internal Vector2 ApplyIceFriction(Vector2 velocity, float dt)
    {
        return velocity * MathF.Pow(FrictionReduction, dt);
    }
}
