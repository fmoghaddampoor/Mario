namespace MarioEngine.Desktop;

using MarioEngine.Core;
using MarioEngine.Core.Graphics;
using MarioEngine.Core.Graphics.Font;
using MarioEngine.Desktop.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// Handles the window Update event. Manages the splash-to-game transition
/// and delegates to game update logic once the game has started.
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
            _logger.LogInformation(Resources.Strings.Splash_Finished);
            _state.Splash.Dispose();
            _state.Splash = null;
            _state.GameStarted = true;
            InitializeRendering();
            _game.Initialize();
            _game.LoadContent();
        }
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
