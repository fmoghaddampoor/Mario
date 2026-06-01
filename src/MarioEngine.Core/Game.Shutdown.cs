namespace MarioEngine.Core;

using Microsoft.Extensions.Logging;

/// <summary>
/// Contains the <see cref="Shutdown"/> method for the <see cref="Game"/> class.
/// Called once when the window is closing to clean up resources.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called once when the window is closing.
    /// Override to save state, unload resources, and clean up.
    /// </summary>
    public virtual void Shutdown()
    {
        _logger.LogInformation("Game shutting down");
    }
}
