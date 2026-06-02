namespace MarioEngine.Core.Physics.Zones;

using System.Numerics;
using MarioEngine.Core.Physics.Collision;

/// <summary>
/// Defines a rectangular area that applies a wind force to objects inside it.
/// </summary>
internal sealed class WindZone
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindZone"/> class.
    /// </summary>
    /// <param name="direction">Wind direction vector.</param>
    /// <param name="strength">Wind force magnitude.</param>
    /// <param name="area">Bounding area.</param>
    internal WindZone(Vector2 direction, float strength, Aabb area)
    {
        Direction = direction;
        Strength = strength;
        Area = area;
    }

    /// <summary>Gets or sets the wind direction vector (does not need to be normalized).</summary>
    internal Vector2 Direction { get; set; }

    /// <summary>Gets or sets the magnitude of the wind force.</summary>
    internal float Strength { get; set; }

    /// <summary>Gets or sets the bounding area of the wind zone.</summary>
    internal Aabb Area { get; set; }

    /// <summary>
    /// Checks whether the given position is inside the wind zone.
    /// </summary>
    /// <param name="position">World position to test.</param>
    /// <returns>True if the position is inside the zone.</returns>
    internal bool Affects(Vector2 position)
    {
        return Area.Contains(position);
    }

    /// <summary>
    /// Returns the wind force vector applied to objects in this zone.
    /// </summary>
    /// <returns>The force as a scaled direction vector.</returns>
    internal Vector2 GetForce()
    {
        return Direction * Strength;
    }
}
