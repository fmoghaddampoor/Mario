namespace MarioEngine.Core.Physics.Zones;

using System.Numerics;

/// <summary>
/// Simulates buoyancy and drag forces for bodies submerged in water.
/// </summary>
internal sealed class WaterPhysics
{
    /// <summary>Gets or sets the upward buoyant force applied when submerged.</summary>
    internal float BuoyancyForce { get; set; } = 200f;

    /// <summary>Gets or sets the linear drag coefficient slowing horizontal/vertical movement.</summary>
    internal float LinearDrag { get; set; } = 0.5f;

    /// <summary>Gets or sets the angular drag coefficient (reserved for rotational damping).</summary>
    internal float AngularDrag { get; set; } = 0.1f;

    /// <summary>
    /// Computes the net buoyancy force acting on a submerged body.
    /// Applies upward force and damps existing velocity.
    /// </summary>
    /// <param name="depth">How deep the body is submerged (0 = surface).</param>
    /// <param name="velocity">Current velocity of the body.</param>
    /// <returns>Combined force from buoyancy and drag.</returns>
    internal Vector2 ApplyBuoyancy(float depth, Vector2 velocity)
    {
        var force = Vector2.Zero;

        if (depth > 0f)
        {
            force.Y -= BuoyancyForce * depth;
        }

        force -= velocity * LinearDrag;
        return force;
    }
}
