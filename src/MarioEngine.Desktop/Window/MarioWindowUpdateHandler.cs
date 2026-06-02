namespace MarioEngine.Desktop;

using MarioEngine.Core;
using MarioEngine.Core.Graphics;
using MarioEngine.Core.Graphics.Font;
using MarioEngine.Core.UI;
using MarioEngine.Desktop.Resources;
using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the window Update event. Manages the splash-to-menu-to-game transition.
/// After the splash screen, shows the main menu before starting the game.
/// </summary>
internal sealed class MarioWindowUpdateHandler
{
    /// <summary>Game instance to update each frame.</summary>
    private readonly Game _game;

    /// <summary>Shared startup state for splash-to-game transition.</summary>
    private readonly GameStartupState _state;

    /// <summary>Logger instance for startup transition events.</summary>
    private readonly ILogger<MarioWindow> _logger;

    /// <summary>Logger factory for creating typed loggers for subsystems.</summary>
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>The MarioWindow providing GL context access.</summary>
    private readonly MarioWindow _window;

    /// <summary>Main menu instance shown after splash.</summary>
    private MainMenu? _mainMenu;

    /// <summary>Whether the game has been initialized (started from menu).</summary>
    private bool _gameInitialized;

    /// <summary>Timer for auto-starting game from menu.</summary>
    private float _menuTimer;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowUpdateHandler"/> class.</summary>
    /// <param name="window">The MarioWindow providing GL context access.</param>
    /// <param name="game">The game instance to update.</param>
    /// <param name="state">Shared startup state between update and render handlers.</param>
    /// <param name="logger">Logger instance.</param>
    /// <param name="loggerFactory">Logger factory for creating typed subsystem loggers.</param>
    public MarioWindowUpdateHandler(MarioWindow window, Game game, GameStartupState state, ILogger<MarioWindow> logger, ILoggerFactory loggerFactory)
    {
        _window = window;
        _game = game;
        _state = state;
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    /// <summary>Called every frame. Updates splash, menu, or game.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Handle(float dt)
    {
        if (_gameInitialized)
        {
            _game.ProcessInput(dt);
            _game.Update(dt);
            return;
        }

        if (_state.GameStarted)
        {
            _menuTimer += dt;
            UpdateMenu(dt);
            return;
        }

        _state.Splash?.Update(dt);

        if (_state.Splash != null && _state.Splash.IsFinished)
        {
            _logger.LogInformation(Resources.Strings.Splash_Finished);
            _state.Splash.Dispose();
            _state.Splash = null;
            _state.GameStarted = true;
            InitializeRendering();
            ShowMainMenu();
        }
    }

    /// <summary>Shows the main menu after the splash screen.</summary>
    private void ShowMainMenu()
    {
        _mainMenu = new MainMenu();
        _game.UI.Show(UIManager.UIState.MainMenu);
        _logger.LogInformation("Main menu displayed");
    }

    /// <summary>Updates the main menu. Auto-starts new game after a brief display.</summary>
    private void UpdateMenu(float dt)
    {
        if (_mainMenu == null)
        {
            return;
        }

        _menuTimer += dt;

        // Auto-start game after 1s (placeholder until InputManager is wired to menu)
        if (_menuTimer >= 1f && !_gameInitialized)
        {
            _logger.LogInformation("Auto-starting game from main menu");
            StartGame();
        }
    }

    /// <summary>Handles the selected menu action.</summary>
    private void HandleMenuConfirm()
    {
        if (_mainMenu == null)
        {
            return;
        }

        var selected = _mainMenu.Items[_mainMenu.SelectedIndex].Label;

        switch (selected)
        {
            case "New Game":
                StartGame();
                break;
            case "Continue":
                StartGame();
                break;
            case "Settings":
                _logger.LogInformation("Settings selected");
                break;
            case "Credits":
                _logger.LogInformation("Credits selected");
                break;
            case "Quit":
                _window.NativeWindow.Close();
                break;
        }
    }

    /// <summary>Starts the actual game after menu selection.</summary>
    private void StartGame()
    {
        _mainMenu = null;
        _game.UI.Hide();
        _gameInitialized = true;
        _logger.LogInformation("Starting game from main menu");
        _game.Initialize();
        _game.LoadContent();
    }

    /// <summary>Creates the rendering pipeline and assigns it to the game instance.</summary>
#pragma warning disable CA2000 // Ownership transferred to Renderer2D
    private void InitializeRendering()
    {
        var gl = _window.GL;
        var batcher = new SpriteBatcher(gl, _loggerFactory.CreateLogger<SpriteBatcher>());
        var renderer = new Renderer2D(batcher, gl, _loggerFactory.CreateLogger<Renderer2D>());
        renderer.Camera.ViewportWidth = _window.FramebufferWidth;
        renderer.Camera.ViewportHeight = _window.FramebufferHeight;
        _game.Renderer = renderer;
    }
#pragma warning restore CA2000
}
