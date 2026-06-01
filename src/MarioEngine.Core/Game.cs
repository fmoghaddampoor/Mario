namespace MarioEngine.Core;

using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// Core game class with lifecycle methods called by the application window.
/// Override to add custom initialization, updating, and rendering logic.
/// </summary>
public class Game
{
    /// <summary>Logger instance for game lifecycle events.</summary>
    private readonly ILogger<Game> _logger;

    /// <summary>Initializes a new instance of the <see cref="Game"/> class.</summary>
    /// <param name="logger">Logger instance for this class.</param>
    public Game(ILogger<Game> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Called once after the window and OpenGL context are created.
    /// Override to load initial resources and set up game state.
    /// </summary>
    public virtual void Initialize()
    {
        _logger.LogInformation("Game initialized");
    }

    /// <summary>
    /// Called once after <see cref="Initialize"/> to load game content
    /// (textures, audio, shaders, etc.).
    /// </summary>
    public virtual void LoadContent()
    {
    }

    /// <summary>
    /// Called every frame before <see cref="Update"/> to process input.
    /// </summary>
    /// <param name="dt">Delta time in seconds for the current frame.</param>
    public virtual void ProcessInput(float dt)
    {
    }

    /// <summary>
    /// Called every frame with a fixed or variable timestep.
    /// Override to update game logic, physics, animations, etc.
    /// </summary>
    /// <param name="dt">Delta time in seconds for the current frame.</param>
    public virtual void Update(float dt)
    {
    }

    /// <summary>
    /// Called every frame after <see cref="Update"/>.
    /// Override to render the current game state.
    /// </summary>
    /// <param name="interpolation">Interpolation factor (0-1) between fixed updates.</param>
    public virtual void Render(float interpolation)
    {
    }

    /// <summary>
    /// Called once when the window is closing.
    /// Override to save state, unload resources, and clean up.
    /// </summary>
    public virtual void Shutdown()
    {
        _logger.LogInformation("Game shutting down");
    }
}
