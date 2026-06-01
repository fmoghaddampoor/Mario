namespace MarioEngine.Core.Graphics.Particles;

using System.Numerics;

/// <summary>
/// Predefined particle effect configurations for common game effects.
/// Each method returns a <see cref="ParticleEmitterConfig"/> ready to use.
/// </summary>
public static class ParticleEffect
{
    /// <summary>Soft upward dust puffs for landing or stomping.</summary>
    /// <returns>A config for dust particles.</returns>
    public static ParticleEmitterConfig Dust() => new ParticleEmitterConfig
    {
        ParticlesPerSecond = 20,
        MaxParticles = 15,
        Speed = 40f,
        SpeedVariance = 20f,
        Lifetime = 0.5f,
        LifetimeVariance = 0.2f,
        StartSize = 4f,
        EndSize = 12f,
        StartColor = 0xCCCCA0FF,
        EndColor = 0x00000000,
        Gravity = new Vector2(0, -30f),
        Spread = 180f,
        EmissionRadius = 2f,
    };

    /// <summary>Rising sparkles for coin collect or power-up.</summary>
    /// <returns>A config for sparkle particles.</returns>
    public static ParticleEmitterConfig Sparkle() => new ParticleEmitterConfig
    {
        ParticlesPerSecond = 30,
        MaxParticles = 20,
        Speed = 80f,
        SpeedVariance = 40f,
        Lifetime = 0.8f,
        LifetimeVariance = 0.3f,
        StartSize = 3f,
        EndSize = 1f,
        StartColor = 0xFFFFDD88,
        EndColor = 0xFFFFDD00,
        Gravity = new Vector2(0, 20f),
        Spread = 360f,
        EmissionRadius = 5f,
    };

    /// <summary>Fire particles for lava, fireball, or burning effects.</summary>
    /// <returns>A config for fire particles.</returns>
    public static ParticleEmitterConfig Flame() => new ParticleEmitterConfig
    {
        ParticlesPerSecond = 40,
        MaxParticles = 30,
        Speed = 60f,
        SpeedVariance = 30f,
        Lifetime = 0.6f,
        LifetimeVariance = 0.2f,
        StartSize = 8f,
        EndSize = 2f,
        StartColor = 0xFF88AAFF,
        EndColor = 0x00000000,
        Gravity = new Vector2(0, -80f),
        Spread = 60f,
        EmissionRadius = 3f,
    };

    /// <summary>Smoke puffs for explosions or death effects.</summary>
    /// <returns>A config for smoke particles.</returns>
    public static ParticleEmitterConfig Smoke() => new ParticleEmitterConfig
    {
        ParticlesPerSecond = 25,
        MaxParticles = 20,
        Speed = 50f,
        SpeedVariance = 25f,
        Lifetime = 1.0f,
        LifetimeVariance = 0.4f,
        StartSize = 5f,
        EndSize = 20f,
        StartColor = 0x88888888,
        EndColor = 0x00000000,
        Gravity = new Vector2(0, -20f),
        Spread = 360f,
        EmissionRadius = 5f,
    };

    /// <summary>Explosion burst for enemy death or block break.</summary>
    /// <returns>A config for explosion particles (one-shot).</returns>
    public static ParticleEmitterConfig Explosion() => new ParticleEmitterConfig
    {
        ParticlesPerSecond = 0,
        MaxParticles = 30,
        Speed = 150f,
        SpeedVariance = 80f,
        Lifetime = 0.5f,
        LifetimeVariance = 0.2f,
        StartSize = 6f,
        EndSize = 1f,
        StartColor = 0xFFFFAA66,
        EndColor = 0x00000000,
        Gravity = new Vector2(0, 100f),
        Spread = 360f,
        EmissionRadius = 2f,
        OneShot = true,
    };
}
