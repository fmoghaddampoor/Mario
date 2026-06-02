namespace MarioEngine.Desktop;

using System;
using MarioEngine.Core;
using MarioEngine.Desktop.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// Wraps a Silk.NET <see cref="IWindow"/> and manages its lifecycle.
/// Provides access to the OpenGL context, window properties, and CLI configuration.
/// Displays a splash screen on startup before handing control to the game.
/// </summary>
internal sealed partial class MarioWindow : IDisposable
{
    /// <summary>Underlying Silk.NET window instance.</summary>
    private readonly IWindow _window;

    /// <summary>Logger instance for window lifecycle events.</summary>
    private readonly ILogger<MarioWindow> _logger;

    /// <summary>Splash display duration in seconds, from config.</summary>
    private readonly float _splashDuration;

    /// <summary>Shared state for the splash-to-game startup transition.</summary>
    private GameStartupState? _startupState;

    /// <summary>Initializes a new instance of the <see cref="MarioWindow"/> class.</summary>
    /// <param name="window">The underlying Silk.NET window this instance wraps.</param>
    /// <param name="logger">Logger instance for window lifecycle events.</param>
    /// <param name="splashDuration">Number of seconds to display the splash screen.</param>
    private MarioWindow(IWindow window, ILogger<MarioWindow> logger, float splashDuration)
    {
        _window = window;
        _logger = logger;
        _splashDuration = splashDuration;
    }

    /// <summary>Occurs when the window or framebuffer is resized.</summary>
    public event Action<int, int>? OnResize;

    /// <summary>Gets or sets the OpenGL context. Set during the window Load event.</summary>
    internal GL? GLContext { get; set; }

    /// <summary>Gets the OpenGL context. Only valid after the window Load event.</summary>
    public GL GL => this.GLContext ?? throw new InvalidOperationException(Resources.Strings.GL_NotAvailable);

    /// <summary>Gets the window width in pixels.</summary>
    public int Width => _window.Size.X;

    /// <summary>Gets the window height in pixels.</summary>
    public int Height => _window.Size.Y;

    /// <summary>Gets the framebuffer width (may differ from window width on HiDPI).</summary>
    public int FramebufferWidth => _window.FramebufferSize.X;

    /// <summary>Gets the framebuffer height (may differ from window height on HiDPI).</summary>
    public int FramebufferHeight => _window.FramebufferSize.Y;

    /// <summary>
    /// Creates a <see cref="MarioWindow"/> from command-line arguments.
    /// </summary>
    /// <param name="args">Command-line arguments from Main().</param>
    /// <param name="logger">Logger instance.</param>
    /// <param name="splashDuration">Number of seconds to display the splash screen.</param>
    /// <returns>A configured MarioWindow ready to run.</returns>
    public static MarioWindow Create(string[] args, ILogger<MarioWindow> logger, float splashDuration = 3f)
    {
        var parsed = CliArgParser.Parse(args);
        var options = WindowOptions.Default;
        GraphicsConfigurator.Configure(options, parsed);

        var nativeWindow = Window.Create(options);

        var marioWindow = new MarioWindow(nativeWindow, logger, splashDuration);

        nativeWindow.Load += new MarioWindowInitializer(
            nativeWindow, marioWindow, logger, parsed.Width, parsed.Height).HandleLoad;

        return marioWindow;
    }

    /// <summary>Runs the window. Blocks until the window closes.</summary>
    public void Run()
    {
        _logger.LogInformation(Resources.Strings.Window_Starting);
        _window.Run();
    }

    /// <summary>Closes the window.</summary>
    public void Close()
    {
        _window.Close();
    }

    /// <summary>Closes the window and cleans up resources.</summary>
    public void Dispose()
    {
        _startupState?.Splash?.Dispose();
        this.GLContext?.Dispose();
        _window.Dispose();
    }

    /// <summary>Raises the <see cref="OnResize"/> event from external handler classes.</summary>
    /// <param name="width">New framebuffer width in pixels.</param>
    /// <param name="height">New framebuffer height in pixels.</param>
    internal void RaiseResize(int width, int height)
    {
        this.OnResize?.Invoke(width, height);
    }

    /// <summary>Creates the splash screen and stores it in the startup state.</summary>
    internal void CreateSplash()
    {
        if (_startupState != null)
        {
            _startupState.Splash = SplashScreen.Create(this.GL, _splashDuration);
        }
    }
}
