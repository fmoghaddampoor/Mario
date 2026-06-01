namespace MarioEngine.Core;

using Microsoft.Extensions.Logging;

/// <summary>
/// Core game class with lifecycle methods called by the application window.
/// Override to add custom initialization, updating, and rendering logic.
/// This class is split across multiple files by lifecycle method.
/// </summary>
public partial class Game
{
    /// <summary>Logger instance for game lifecycle events.</summary>
    private readonly ILogger<Game> _logger;

    /// <summary>Initializes a new instance of the <see cref="Game"/> class.</summary>
    /// <param name="logger">Logger instance for this class.</param>
    public Game(ILogger<Game> logger)
    {
        _logger = logger;
    }
}
