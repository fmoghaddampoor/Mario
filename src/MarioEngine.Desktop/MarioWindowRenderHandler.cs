namespace MarioEngine.Desktop;

using MarioEngine.Core;

/// <summary>
/// Handles the window Render event. Renders the splash screen
/// while it is active, then delegates to game rendering.
/// </summary>
internal sealed class MarioWindowRenderHandler
{
    /// <summary>Game instance to render each frame.</summary>
    private readonly Game _game;

    /// <summary>Shared startup state for splash-to-game transition.</summary>
    private readonly GameStartupState _state;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowRenderHandler"/> class.</summary>
    /// <param name="game">The game instance to render.</param>
    /// <param name="state">Shared startup state between update and render handlers.</param>
    public MarioWindowRenderHandler(Game game, GameStartupState state)
    {
        _game = game;
        _state = state;
    }

    /// <summary>Called every frame. Renders splash or game.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Handle(float dt)
    {
        Time.Update(dt);

        if (!_state.GameStarted)
        {
            _state.Splash?.Render();
            return;
        }

        _game.Render(0f);
    }
}
