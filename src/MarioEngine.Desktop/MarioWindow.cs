namespace MarioEngine.Desktop;

using System;
using MarioEngine.Core;
using Microsoft.Extensions.Logging;
using Serilog;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// Wraps a Silk.NET <see cref="IWindow"/> and manages its lifecycle.
/// Provides access to the OpenGL context, window properties, and CLI configuration.
/// </summary>
internal sealed class MarioWindow : IDisposable
{
    private readonly IWindow _window;
    private readonly ILogger<MarioWindow> _logger;
    private GL? _gl;

    private MarioWindow(IWindow window, ILogger<MarioWindow> logger)
    {
        _window = window;
        _logger = logger;
    }

    /// <summary>Occurs when the window or framebuffer is resized.</summary>
    public event Action<int, int>? OnResize;

    /// <summary>Gets the OpenGL context. Only valid after the window Load event.</summary>
    public GL GL => _gl ?? throw new InvalidOperationException("OpenGL context not available until window is loaded");

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
    /// <returns>A configured MarioWindow ready to run.</returns>
    public static MarioWindow Create(string[] args, ILogger<MarioWindow> logger)
    {
        var parsed = ParseArgs(args);
        var options = WindowOptions.Default;

        options.Title = "Super Mario \u2014 v" + VersionInfo.Current;
        options.Size = new Silk.NET.Maths.Vector2D<int>(parsed.Width, parsed.Height);
        options.WindowBorder = parsed.Fullscreen ? WindowBorder.Hidden : WindowBorder.Resizable;
        options.VSync = true;
        options.API = new GraphicsAPI(
            ContextAPI.OpenGL,
            ContextProfile.Core,
            ContextFlags.ForwardCompatible,
            new APIVersion(4, 6));

        var nativeWindow = Window.Create(options);

        if (parsed.Fullscreen)
        {
            nativeWindow.WindowState = WindowState.Fullscreen;
        }

        var marioWindow = new MarioWindow(nativeWindow, logger);

        nativeWindow.Load += () =>
        {
            marioWindow._gl = nativeWindow.CreateOpenGL();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Window opened: {Width}x{Height}, GL {Major}.{Minor}",
                    parsed.Width,
                    parsed.Height,
                    4,
                    6);
            }

            nativeWindow.FramebufferResize += (size) =>
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("Framebuffer resized: {Width}x{Height}", size.X, size.Y);
                }

                marioWindow.OnResize?.Invoke(size.X, size.Y);
            };
        };

        return marioWindow;
    }

    /// <summary>Runs the window. Blocks until the window closes.</summary>
    public void Run()
    {
        _logger.LogInformation("Window starting");
        _window.Run();
    }

    /// <summary>Closes the window and cleans up resources.</summary>
    public void Dispose()
    {
        _gl?.Dispose();
        _window.Dispose();
    }

    /// <summary>
    /// Subscribes game lifecycle methods to the corresponding window events.
    /// </summary>
    /// <param name="game">The game instance to wire up.</param>
    public void WireGameEvents(Game game)
    {
        _window.Load += () =>
        {
            game.Initialize();
            game.LoadContent();
        };

        _window.Update += (dt) =>
        {
            game.ProcessInput((float)dt);
            game.Update((float)dt);
        };

        _window.Render += (dt) =>
        {
            Time.Update((float)dt);
            game.Render(0f);
        };

        _window.Closing += () =>
        {
            game.Shutdown();
        };
    }

    /// <summary>
    /// Parses command-line arguments for window configuration.
    /// </summary>
    /// <param name="args">Command-line arguments array.</param>
    /// <returns>Tuple of (fullscreen, width, height).</returns>
    private static (bool Fullscreen, int Width, int Height) ParseArgs(string[] args)
    {
        var fullscreen = false;
        var width = 1280;
        var height = 720;

        for (var i = 0; i < args.Length; i++)
        {
            switch (args[i].ToUpperInvariant())
            {
                case "--FULLSCREEN":
                case "-F":
                    fullscreen = true;
                    break;
                case "--WIDTH":
                case "-W":
                    if (++i < args.Length && int.TryParse(args[i], out var w))
                    {
                        width = w;
                    }

                    break;
                case "--HEIGHT":
                case "-H":
                    if (++i < args.Length && int.TryParse(args[i], out var h))
                    {
                        height = h;
                    }

                    break;
            }
        }

        return (fullscreen, width, height);
    }
}
