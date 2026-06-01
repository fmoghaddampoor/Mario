namespace MarioEngine.Core.Graphics.Particles;

using System.Numerics;

/// <summary>
/// Configuration for a particle emitter. Defines emission rate, particle properties,
/// gravity, spread, and lifetime. Used by <see cref="ParticleSystem.Emit"/>.
/// </summary>
public struct ParticleEmitterConfig
{
    /// <summary>Particles emitted per second (for looping emitters).</summary>
    public float ParticlesPerSecond;

    /// <summary>Maximum number of particles alive at once.</summary>
    public int MaxParticles;

    /// <summary>Particle initial speed in pixels per second.</summary>
    public float Speed;

    /// <summary>Random speed variance (±).</summary>
    public float SpeedVariance;

    /// <summary>Particle lifetime in seconds.</summary>
    public float Lifetime;

    /// <summary>Random lifetime variance (±).</summary>
    public float LifetimeVariance;

    /// <summary>Initial particle size in pixels.</summary>
    public float StartSize;

    /// <summary>Final particle size in pixels (lerped from start).</summary>
    public float EndSize;

    /// <summary>Start color as RGBA uint.</summary>
    public uint StartColor;

    /// <summary>End color as RGBA uint (lerped from start).</summary>
    public uint EndColor;

    /// <summary>Gravity applied per second (pixels/s²).</summary>
    public Vector2 Gravity;

    /// <summary>Spread angle in degrees (0 = all same direction, 360 = full circle).</summary>
    public float Spread;

    /// <summary>Emission radius for circle pattern.</summary>
    public float EmissionRadius;

    /// <summary>One-shot: emit all particles at once. Looping: emit over time.</summary>
    public bool OneShot;
}
