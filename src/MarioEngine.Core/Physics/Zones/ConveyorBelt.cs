namespace MarioEngine.Core.Physics.Zones;

using System.Numerics;
using MarioEngine.Core.Physics.Collision;

/// <summary>
/// A conveyor belt zone that imparts surface velocity to objects resting on it.
/// </summary>
internal sealed class ConveyorBelt
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConveyorBelt"/> class.
    /// </summary>
    /// <param name="speed">Belt speed in units/s.</param>
    /// <param name="active">Whether the belt starts active.</param>
    /// <param name="area">Bounding area of the belt surface.</param>
    internal ConveyorBelt(float speed, bool active, Aabb area)
    {
        Speed = speed;
        Active = active;
        Area = area;
    }

    /// <summary>Gets or sets the speed of the belt surface in units/s.</summary>
    internal float Speed { get; set; }

    /// <summary>Gets or sets a value indicating whether the conveyor belt is currently running.</summary>
    internal bool Active { get; set; }

    /// <summary>Gets or sets the bounding area of the conveyor belt surface.</summary>
    internal Aabb Area { get; set; }

    /// <summary>
    /// Computes the surface velocity at the given position on the belt.
    /// Direction is rightward by default; override by rotating the area.
    /// </summary>
    /// <param name="position">World position to evaluate.</param>
    /// <returns>Surface velocity vector, or zero if inactive or out of bounds.</returns>
    internal Vector2 GetSurfaceVelocity(Vector2 position)
    {
        if (!Active || !Area.Contains(position))
        {
            return Vector2.Zero;
        }

        return new Vector2(Speed, 0f);
    }
}
