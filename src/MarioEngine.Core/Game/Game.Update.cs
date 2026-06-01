namespace MarioEngine.Core;

/// <summary>
/// Contains the <see cref="Update"/> method for the <see cref="Game"/> class.
/// Called every frame to advance game logic, physics, and animations.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called every frame with a fixed or variable timestep.
    /// Override to update game logic, physics, animations, etc.
    /// Call base.Update(dt) to keep performance metrics running.
    /// </summary>
    /// <param name="dt">Delta time in seconds for the current frame.</param>
    public virtual void Update(float dt)
    {
        _metrics.Update(dt);
    }
}
