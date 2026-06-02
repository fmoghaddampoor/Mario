namespace MarioEngine.Core.Physics;

using System.Numerics;

/// <summary>
/// A platform that moves between defined waypoints at a fixed speed.
/// Can optionally loop back to the first waypoint.
/// </summary>
internal sealed class MovingPlatform
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MovingPlatform"/> class.
    /// </summary>
    /// <param name="startPosition">Initial world position.</param>
    /// <param name="waypoints">Target waypoints.</param>
    /// <param name="speed">Movement speed.</param>
    /// <param name="loop">Whether the path loops.</param>
    internal MovingPlatform(Vector2 startPosition, Vector2[] waypoints, float speed, bool loop)
    {
        Position = startPosition;
        Waypoints = waypoints;
        Speed = speed;
        Loop = loop;
        CurrentIndex = 0;
    }

    /// <summary>Gets or sets the ordered list of waypoint positions the platform travels between.</summary>
    internal Vector2[]? Waypoints { get; set; }

    /// <summary>Gets or sets the movement speed in units/s.</summary>
    internal float Speed { get; set; }

    /// <summary>Gets or sets a value indicating whether the platform loops back to the first waypoint.</summary>
    internal bool Loop { get; set; }

    /// <summary>Gets or sets the index of the current target waypoint.</summary>
    internal int CurrentIndex { get; set; }

    /// <summary>Gets the current world position of the platform.</summary>
    internal Vector2 Position { get; private set; }

    /// <summary>Gets the current velocity of the platform.</summary>
    internal Vector2 Velocity { get; private set; }

    /// <summary>
    /// Advances the platform along its waypoint path by the given delta time.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    internal void Update(float dt)
    {
        if (Waypoints is null || Waypoints.Length == 0)
        {
            Velocity = Vector2.Zero;
            return;
        }

        var target = Waypoints[CurrentIndex];
        var direction = target - Position;
        var distance = direction.Length();

        if (distance <= 0.001f)
        {
            Position = target;
            Velocity = Vector2.Zero;

            if (CurrentIndex + 1 < Waypoints.Length)
            {
                CurrentIndex++;
            }
            else if (Loop)
            {
                CurrentIndex = 0;
            }

            return;
        }

        var step = Speed * dt;
        direction /= distance;

        if (step >= distance)
        {
            Position = target;
            Velocity = direction * (distance / dt);
        }
        else
        {
            Position += direction * step;
            Velocity = direction * Speed;
        }
    }
}
