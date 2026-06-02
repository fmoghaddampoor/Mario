namespace MarioEngine.Core.Physics.Body;

using System.Numerics;

/// <summary>
/// Physics body with position, velocity, mass, and force accumulation.
/// Updated by <see cref="PhysicsSystem"/> each frame.
/// </summary>
internal sealed class RigidBody
{
    /// <summary>Gravity acceleration in units/s² applied to this body.</summary>
    private Vector2 _gravity = new Vector2(0f, 980f);

    /// <summary>Current velocity in units/s.</summary>
    private Vector2 _velocity;

    /// <summary>Accumulated forces to apply this frame (cleared after each step).</summary>
    private Vector2 _forceAccumulator;

    /// <summary>Mass of the body in kg. 0 = static (immovable).</summary>
    private float _mass = 1f;

    /// <summary>Inverse mass (1/mass). 0 for static bodies.</summary>
    private float _inverseMass = 1f;

    /// <summary>Linear damping factor (0.0 to 1.0).</summary>
    private float _damping = 0.1f;

    /// <summary>Gets or sets the current velocity in units/s.</summary>
    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    /// <summary>Gets or sets the mass. Setting to 0 makes the body static.</summary>
    public float Mass
    {
        get => _mass;
        set
        {
            _mass = MathF.Max(0f, value);
            _inverseMass = _mass > 0f ? 1f / _mass : 0f;
        }
    }

    /// <summary>Gets a value indicating whether this body is static (immovable).</summary>
    public bool IsStatic => _mass <= 0f;

    /// <summary>Gets the inverse mass (0 for static bodies).</summary>
    public float InverseMass => _inverseMass;

    /// <summary>Gets or sets the gravity acceleration applied to this body.</summary>
    public Vector2 Gravity
    {
        get => _gravity;
        set => _gravity = value;
    }

    /// <summary>Gets or sets the linear damping factor.</summary>
    public float Damping
    {
        get => _damping;
        set => _damping = Math.Clamp(value, 0f, 1f);
    }

    /// <summary>Applies an instantaneous force.</summary>
    /// <param name="force">Force vector in units/s².</param>
    public void AddForce(Vector2 force)
    {
        _forceAccumulator += force;
    }

    /// <summary>Integrates velocity and position using semi-implicit Euler.</summary>
    /// <param name="dt">Fixed timestep in seconds.</param>
    public void Integrate(float dt)
    {
        if (IsStatic)
        {
            return;
        }

        _velocity += _gravity * dt;
        _velocity += _forceAccumulator * _inverseMass * dt;
        _velocity *= 1f - _damping;
        _forceAccumulator = Vector2.Zero;
    }
}
