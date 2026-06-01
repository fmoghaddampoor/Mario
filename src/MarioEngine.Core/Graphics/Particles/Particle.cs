namespace MarioEngine.Core.Graphics.Particles;

using System.Numerics;

/// <summary>
/// A single particle with full state including initial values for interpolation.
/// Managed by <see cref="ParticleSystem"/> in a pooled list.
/// </summary>
public struct Particle
{
    /// <summary>Current world position in pixels.</summary>
    public Vector2 Position;

    /// <summary>Velocity in pixels per second.</summary>
    public Vector2 Velocity;

    /// <summary>Current RGBA color packed as uint (0xRRGGBBAA).</summary>
    public uint Color;

    /// <summary>Current size in pixels.</summary>
    public float Size;

    /// <summary>Remaining lifetime in seconds. Dead when &lt;= 0.</summary>
    public float Lifetime;

    /// <summary>Initial lifetime for normalized interpolation.</summary>
    public float InitialLifetime;

    /// <summary>Start color for interpolation.</summary>
    public uint StartColor;

    /// <summary>End color for interpolation.</summary>
    public uint EndColor;

    /// <summary>Start size for interpolation.</summary>
    public float StartSize;

    /// <summary>End size for interpolation.</summary>
    public float EndSize;

    /// <summary>Gravity applied each frame.</summary>
    public Vector2 Gravity;

    /// <summary>Gets a value indicating whether the particle is dead and can be recycled.</summary>
    public readonly bool IsDead => Lifetime <= 0f;
}
