namespace MarioEngine.Core;

using Microsoft.Extensions.Logging;

/// <summary>
/// Contains the <see cref="LoadContent"/> method for the <see cref="Game"/> class.
/// Called once after <see cref="Game.Initialize"/> to load game content.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called once after <see cref="Initialize"/> to load game content
    /// (textures, audio, shaders, etc.).
    /// </summary>
    public virtual void LoadContent()
    {
        _logger.LogInformation("Game.LoadContent started");
    }
}
