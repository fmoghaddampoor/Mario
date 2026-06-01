namespace MarioEngine.Core;

using MarioEngine.Core.Resources;
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
    /// Call base.Shutdown() to dispose the audio system.
    /// </summary>
    public virtual void Shutdown()
    {
        _logger.LogInformation(Resources.Strings.Game_Shutdown_Started);
        _audio.Dispose();
        _logger.LogInformation(Resources.Strings.Game_ShuttingDown);
    }
}
