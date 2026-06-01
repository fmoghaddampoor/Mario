namespace MarioEngine.Core.Graphics.Particles;

using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.Logging;

/// <summary>
/// Manages a pooled particle system. Updates particles, emits new ones,
/// applies modifiers, and prepares data for batched rendering.
/// Supports 1000+ particles at 60 FPS with zero per-frame allocations.
/// </summary>
public sealed partial class ParticleSystem
{
    /// <summary>Logger for debug output.</summary>
    private readonly ILogger<ParticleSystem> _logger;

    /// <summary>Pool of alive particles. Dead particles are removed during update.</summary>
    private readonly List<Particle> _particles;

    /// <summary>Accumulated fractional particles for smooth emission rates.</summary>
    private float _accumulator;

    /// <summary>Initializes a new instance of the <see cref="ParticleSystem"/> class.</summary>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if logger is null.</exception>
    public ParticleSystem(ILogger<ParticleSystem> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _particles = new List<Particle>(512);
    }

    /// <summary>Gets the current list of alive particles for rendering.</summary>
    public IReadOnlyList<Particle> Particles => _particles;

    /// <summary>Gets the number of currently alive particles.</summary>
    public int AliveCount => _particles.Count;
}
