namespace MarioEngine.Core.Physics.Debug;

using System.Numerics;
using MarioEngine.Core.Physics.Body;

/// <summary>
/// Static serializer for converting <see cref="RigidBody"/> state to and from strings.
/// Uses a simple CSV-like format for debug logging and replay.
/// </summary>
internal static class PhysicsSerializer
{
    /// <summary>
    /// Serializes a rigidbody's position, velocity, and mass to a string.
    /// </summary>
    /// <param name="body">The rigidbody to serialize.</param>
    /// <returns>A delimited string representation of the body state.</returns>
    internal static string Serialize(RigidBody body)
    {
        return $"{body.Velocity.X},{body.Velocity.Y},{body.Mass}";
    }

    /// <summary>
    /// Deserializes a string back into a <see cref="RigidBody"/> state.
    /// Returns null if parsing fails.
    /// </summary>
    /// <param name="data">The delimited string from <see cref="Serialize"/>.</param>
    /// <returns>A new RigidBody, or null if data was malformed.</returns>
    internal static RigidBody? Deserialize(string data)
    {
        var parts = data.Split(',');
        if (parts.Length < 3)
        {
            return null;
        }

        if (!float.TryParse(parts[0], out var vx) ||
            !float.TryParse(parts[1], out var vy) ||
            !float.TryParse(parts[2], out var mass))
        {
            return null;
        }

        var body = new RigidBody
        {
            Velocity = new Vector2(vx, vy),
            Mass = mass,
        };

        return body;
    }
}
