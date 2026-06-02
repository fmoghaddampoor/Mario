namespace MarioEngine.Core.Physics;

using System.Numerics;

/// <summary>
/// Controls the physics of a Koopa-like shell that can be kicked,
/// spin in place, and collide with other entities.
/// </summary>
internal sealed class ShellPhysics
{
    /// <summary>Gets or sets the current movement speed in units/s.</summary>
    internal float Speed { get; set; }

    /// <summary>Gets or sets a value indicating whether the shell is spinning (active and dangerous).</summary>
    internal bool Spinning { get; set; }

    /// <summary>Gets or sets the current movement direction.</summary>
    internal Vector2 Direction { get; set; }

    /// <summary>
    /// Launches the shell in a given direction at the given speed.
    /// </summary>
    /// <param name="dir">Normalized direction vector.</param>
    /// <param name="speed">Speed in units/s.</param>
    internal void Launch(Vector2 dir, float speed)
    {
        Direction = Vector2.Normalize(dir);
        Speed = speed;
        Spinning = true;
    }

    /// <summary>
    /// Stops the shell completely. Resets speed and spinning state.
    /// </summary>
    internal void Stop()
    {
        Speed = 0f;
        Spinning = false;
        Direction = Vector2.Zero;
    }

    /// <summary>
    /// Advances the shell's internal state. Reserved for future physics updates.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    internal void Update(float dt)
    {
        _ = dt;
    }
}
