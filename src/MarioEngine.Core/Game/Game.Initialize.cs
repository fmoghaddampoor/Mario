namespace MarioEngine.Core;

using MarioEngine.Core.Resources;
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
    /// Call base.Initialize() to initialize the audio system.
    /// </summary>
    public virtual void Initialize()
    {
        _logger.LogInformation(Resources.Strings.Game_Initializing);
        _audio.Initialize();
        _logger.LogInformation(Resources.Strings.Game_Initialized);
    }
}
