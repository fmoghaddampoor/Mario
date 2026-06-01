namespace MarioEngine.Core.Graphics.Particles;

using System.Numerics;

/// <summary>
/// Contains emission methods (Emit, EmitBurst, EmitOne) for <see cref="ParticleSystem"/>.
/// </summary>
public sealed partial class ParticleSystem
{
    /// <summary>
    /// Emits particles from the given emitter configuration.
    /// Call once per frame for looping emitters.
    /// </summary>
    /// <param name="config">Emitter configuration.</param>
    /// <param name="position">World position of the emitter in pixels.</param>
    /// <param name="dt">Delta time in seconds.</param>
    public void Emit(ParticleEmitterConfig config, Vector2 position, float dt)
    {
        if (config.OneShot)
        {
            EmitBurst(config, position);
            return;
        }

        _accumulator += config.ParticlesPerSecond * dt;
        var count = (int)_accumulator;
        _accumulator -= count;

        for (var i = 0; i < count; i++)
        {
            EmitOne(config, position);
        }
    }

    /// <summary>
    /// Emits a burst of particles immediately (for one-shot effects).
    /// </summary>
    /// <param name="config">Emitter configuration.</param>
    /// <param name="position">World position in pixels.</param>
    public void EmitBurst(ParticleEmitterConfig config, Vector2 position)
    {
        for (var i = 0; i < config.MaxParticles; i++)
        {
            EmitOne(config, position);
        }
    }

#pragma warning disable CA5394 // Random used for visual effects, not security
    private void EmitOne(ParticleEmitterConfig config, Vector2 position)
    {
        if (_particles.Count >= config.MaxParticles)
        {
            return;
        }

        var rand = new Random();
        var angleDeg = (float)((rand.NextDouble() * config.Spread) - (config.Spread * 0.5));
        var angleRad = angleDeg * (MathF.PI / 180f);
        var speed = config.Speed + ((float)((rand.NextDouble() * 2.0) - 1.0) * config.SpeedVariance);
        var lifetime = config.Lifetime + ((float)((rand.NextDouble() * 2.0) - 1.0) * config.LifetimeVariance);

        var pos = position;
        if (config.EmissionRadius > 0f)
        {
            var ra = (float)(rand.NextDouble() * Math.PI * 2.0);
            var rd = (float)rand.NextDouble() * config.EmissionRadius;
            pos += new Vector2(MathF.Cos(ra) * rd, MathF.Sin(ra) * rd);
        }

        _particles.Add(new Particle
        {
            Position = pos,
            Velocity = new Vector2(MathF.Cos(angleRad) * speed, MathF.Sin(angleRad) * speed),
            Color = config.StartColor,
            Size = config.StartSize,
            Lifetime = lifetime,
            InitialLifetime = lifetime,
            StartColor = config.StartColor,
            EndColor = config.EndColor,
            StartSize = config.StartSize,
            EndSize = config.EndSize,
            Gravity = config.Gravity,
        });
    }

#pragma warning restore CA5394
}