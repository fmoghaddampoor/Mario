namespace MarioEngine.Desktop;

using MarioEngine.Core;
using MarioEngine.Core.Graphics;
using MarioEngine.Core.Graphics.Font;
using MarioEngine.Core.UI;
using MarioEngine.Desktop.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// Handles the window Update event. Manages the splash-to-menu-to-game transition.
/// GL operations are deferred to the Render handler via a pending flag.
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

    /// <summary>True when splash has finished and rendering init is pending.</summary>
    private bool _pendingInit;

    /// <summary>Main menu instance shown after splash.</summary>
    private MainMenu? _mainMenuInstance;

    /// <summary>Whether the game has been initialized (started from menu).</summary>
    private bool _gameInitialized;

    /// <summary>Timer for auto-starting game from menu.</summary>
    private float _menuTimer;

    /// <summary>Gets or sets whether rendering initialization is pending.</summary>
    internal bool PendingInit
    {
        get => _pendingInit;
        set => _pendingInit = value;
    }

    /// <summary>Gets or sets the main menu instance.</summary>
    internal MainMenu? MainMenuInstance
    {
        get => _mainMenuInstance;
        set => _mainMenuInstance = value;
    }

    /// <summary>Gets or sets whether the game has been initialized.</summary>
    internal bool GameInitialized
    {
        get => _gameInitialized;
        set => _gameInitialized = value;
    }

    /// <summary>Gets or sets the menu timer value.</summary>
    internal float MenuTimer
    {
        get => _menuTimer;
        set => _menuTimer = value;
    }

    /// <summary>Adds delta time to the menu timer.</summary>
    internal void AddMenuTimer(float dt) => _menuTimer += dt;

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
        if (GameInitialized)
        {
            _game.ProcessInput(dt);
            _game.Update(dt);
            return;
        }

        if (_state.GameStarted)
        {
            MenuTimer += dt;
            return;
        }

        _state.Splash?.Update(dt);

        if (_state.Splash != null && _state.Splash.IsFinished)
        {
            _logger.LogInformation(Resources.Strings.Splash_Finished);
            _state.Splash = null;
            _state.GameStarted = true;
            InitializeRendering();
            ShowMainMenu();
        }
    }

    /// <summary>Creates the rendering pipeline and assigns it to the game instance. Call from Render event.</summary>
#pragma warning disable CA2000 // Ownership transferred to Renderer2D
    internal unsafe void InitializeRendering()
    {
        var gl = _window.GL;

        // Create a minimal shader program manually (inline to avoid issues with file loading during Update loop)
        var vsHandle = gl.CreateShader(ShaderType.VertexShader);
        var vsSource = @"#version 330 core
layout(location = 0) in vec2 aPos;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec4 aColor;
out vec2 vTexCoord;
out vec4 vColor;
void main() { gl_Position = vec4(aPos, 0.0, 1.0); vTexCoord = aTexCoord; vColor = aColor; }";
        gl.ShaderSource(vsHandle, vsSource);
        gl.CompileShader(vsHandle);

        var fsHandle = gl.CreateShader(ShaderType.FragmentShader);
        var fsSource = @"#version 330 core
in vec2 vTexCoord;
in vec4 vColor;
out vec4 fragColor;
uniform sampler2D uTexture;
void main() { fragColor = texture(uTexture, vTexCoord) * vColor; }";
        gl.ShaderSource(fsHandle, fsSource);
        gl.CompileShader(fsHandle);

        var program = gl.CreateProgram();
        gl.AttachShader(program, vsHandle);
        gl.AttachShader(program, fsHandle);
        gl.LinkProgram(program);
        gl.DeleteShader(vsHandle);
        gl.DeleteShader(fsHandle);

        var batcher = new SpriteBatcher(gl, _loggerFactory.CreateLogger<SpriteBatcher>());
        batcher.ShaderProgram = program;
        var renderer = new Renderer2D(batcher, gl, _loggerFactory.CreateLogger<Renderer2D>());
        renderer.Camera.ViewportWidth = _window.FramebufferWidth;
        renderer.Camera.ViewportHeight = _window.FramebufferHeight;
        _game.Renderer = renderer;
    }
#pragma warning restore CA2000

    internal void ShowMainMenu()
    {
        MainMenuInstance = new MainMenu();
        _game.UI.Show(UIManager.UIState.MainMenu);
        _logger.LogInformation("Main menu displayed");
    }

    internal void StartGame()
    {
        MainMenuInstance = null;
        _game.UI.Hide();
        GameInitialized = true;
        _logger.LogInformation("Starting game from main menu");
        _game.Initialize();
        _game.LoadContent();
    }
}
