namespace MarioEngine.Desktop;

using MarioEngine.Core;

/// <summary>
/// Handles the window Render event. Renders the splash screen
/// with correct aspect ratio, then delegates to game rendering.
/// Also handles deferred rendering initialization (GL ops only allowed in Render callback).
/// </summary>
internal sealed class MarioWindowRenderHandler
{
    /// <summary>MarioWindow for accessing framebuffer dimensions.</summary>
    private readonly MarioWindow _window;

    /// <summary>Game instance to render each frame.</summary>
    private readonly Game _game;

    /// <summary>Shared startup state for splash-to-game transition.</summary>
    private readonly GameStartupState _state;

    /// <summary>Reference to the update handler for deferred GL init.</summary>
    private readonly MarioWindowUpdateHandler _updateHandler;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowRenderHandler"/> class.</summary>
    /// <param name="window">MarioWindow for framebuffer size access.</param>
    /// <param name="game">The game instance to render.</param>
    /// <param name="state">Shared startup state between update and render handlers.</param>
    /// <param name="updateHandler">Update handler for deferred GL initialization.</param>
    public MarioWindowRenderHandler(MarioWindow window, Game game, GameStartupState state, MarioWindowUpdateHandler updateHandler)
    {
        _window = window;
        _game = game;
        _state = state;
        _updateHandler = updateHandler;
    }

    /// <summary>Called every frame. Renders splash or game.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Handle(float dt)
    {
        Time.Update(dt);

        // Handle deferred rendering initialization (GL ops only allowed in Render callback)
        if (_updateHandler.PendingInit)
        {
            _updateHandler.PendingInit = false;
            _updateHandler.InitializeRendering();
            _updateHandler.ShowMainMenu();
        }

        // Auto-start game from menu (runs here too since menu input is deferred)
        if (_updateHandler.MainMenuInstance != null && !_updateHandler.GameInitialized)
        {
            _updateHandler.AddMenuTimer(dt);
            if (_updateHandler.MenuTimer >= 5f)
            {
                _updateHandler.StartGame();
            }
        }

        if (!_state.GameStarted)
        {
            _state.Splash?.Render(_window.FramebufferWidth, _window.FramebufferHeight);
            return;
        }

        _game.Render(0f);
    }
}
