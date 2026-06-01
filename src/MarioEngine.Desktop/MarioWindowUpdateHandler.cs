namespace MarioEngine.Desktop;

using MarioEngine.Core;
using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the window Update event. Manages the splash-to-game transition
/// and delegates to game update logic once the game has started.
/// </summary>
internal sealed class MarioWindowUpdateHandler
{
    private readonly Game _game;
    private readonly GameStartupState _state;
    private readonly ILogger<MarioWindow> _logger;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowUpdateHandler"/> class.</summary>
    /// <param name="game">The game instance to update.</param>
    /// <param name="state">Shared startup state between update and render handlers.</param>
    /// <param name="logger">Logger instance.</param>
    public MarioWindowUpdateHandler(Game game, GameStartupState state, ILogger<MarioWindow> logger)
    {
        _game = game;
        _state = state;
        _logger = logger;
    }

    /// <summary>Called every frame. Updates splash or game.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Handle(float dt)
    {
        if (_state.GameStarted)
        {
            _game.ProcessInput(dt);
            _game.Update(dt);
            return;
        }

        _state.Splash?.Update(dt);

        if (_state.Splash != null && _state.Splash.IsFinished)
        {
            _logger.LogInformation("Splash finished, starting game");
            _state.Splash.Dispose();
            _state.Splash = null;
            _state.GameStarted = true;
            _game.Initialize();
            _game.LoadContent();
        }
    }
}
