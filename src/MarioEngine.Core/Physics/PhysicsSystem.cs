namespace MarioEngine.Core.Physics;

using System;
using System.Collections.Generic;
using MarioEngine.Core.Physics.Body;
using MarioEngine.Core.Physics.Collision;
using Microsoft.Extensions.Logging;

/// <summary>
/// Core physics simulation system.
/// Runs at a fixed timestep (240 Hz) for deterministic behavior.
/// Manages rigidbodies, collision detection, gravity, and position correction.
/// </summary>
internal sealed class PhysicsSystem : IDisposable
{
    /// <summary>Fixed timestep in seconds (240 Hz).</summary>
    private const float FixedDt = 1f / 240f;

    /// <summary>Maximum number of physics steps per frame to prevent spiral of death.</summary>
    private const int MaxStepsPerFrame = 10;

    /// <summary>Logger for physics events.</summary>
    private readonly ILogger _logger;

    /// <summary>All registered rigidbodies.</summary>
    private readonly List<RigidBody> _bodies;

    /// <summary>Spatial hash grid for broad-phase.</summary>
    private readonly SpatialHash _spatialHash;

    /// <summary>Accumulator for fixed timestep.</summary>
    private float _accumulator;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="PhysicsSystem"/> class.</summary>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if logger is null.</exception>
    public PhysicsSystem(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _bodies = new List<RigidBody>();
        _spatialHash = new SpatialHash(100f);
    }

    /// <summary>Registers a rigidbody for physics simulation.</summary>
    /// <param name="body">The body to add.</param>
    public void AddBody(RigidBody body)
    {
        _bodies.Add(body);
    }

    /// <summary>Removes a rigidbody from simulation.</summary>
    /// <param name="body">The body to remove.</param>
    public void RemoveBody(RigidBody body)
    {
        _bodies.Remove(body);
    }

    /// <summary>
    /// Steps the physics simulation forward by a variable dt.
    /// Uses a fixed timestep accumulator for stable simulation.
    /// </summary>
    /// <param name="dt">Delta time in seconds since the last frame.</param>
    public void Update(float dt)
    {
        _accumulator += dt;
        var steps = 0;

        while (_accumulator >= FixedDt && steps < MaxStepsPerFrame)
        {
            Step();
            _accumulator -= FixedDt;
            steps++;
        }

        if (steps >= MaxStepsPerFrame)
        {
            _accumulator = 0f;
        }
    }

    /// <summary>Performs a single fixed-timestep physics step.</summary>
    private void Step()
    {
        _spatialHash.Clear();

        foreach (var body in _bodies)
        {
            body.Integrate(FixedDt);
        }

        // Broad phase + narrow phase collision detection would go here
        // when collider components are added
    }

    /// <summary>Releases all physics resources.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _bodies.Clear();
    }
}
