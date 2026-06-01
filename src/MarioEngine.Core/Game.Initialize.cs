namespace MarioEngine.Core;

using Microsoft.Extensions.Logging;

/// <summary>
/// Contains the <see cref="Initialize"/> method for the <see cref="Game"/> class.
/// Called once after the window and OpenGL context are created.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called once after the window and OpenGL context are created.
    /// Override to load initial resources and set up game state.
    /// </summary>
    public virtual void Initialize()
    {
        _logger.LogInformation("Game initializing");
        _logger.LogInformation("Game initialized");
    }
}
