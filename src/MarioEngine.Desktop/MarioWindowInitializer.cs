namespace MarioEngine.Desktop;

using MarioEngine.Desktop.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// Handles the native window Load event. Creates the OpenGL context,
/// logs window information, and subscribes to framebuffer resize events.
/// </summary>
internal sealed class MarioWindowInitializer
{
    /// <summary>The underlying Silk.NET window used for creating the OpenGL context.</summary>
    private readonly IWindow _nativeWindow;

    /// <summary>The MarioWindow wrapper to set the GL context on and notify of resize events.</summary>
    private readonly MarioWindow _marioWindow;

    /// <summary>Logger instance for window initialization and resize events.</summary>
    private readonly ILogger<MarioWindow> _logger;

    /// <summary>Window width in pixels for logging purposes.</summary>
    private readonly int _width;

    /// <summary>Window height in pixels for logging purposes.</summary>
    private readonly int _height;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowInitializer"/> class.</summary>
    /// <param name="nativeWindow">The underlying Silk.NET window.</param>
    /// <param name="marioWindow">The MarioWindow wrapper instance.</param>
    /// <param name="logger">Logger instance.</param>
    /// <param name="width">Window width in pixels.</param>
    /// <param name="height">Window height in pixels.</param>
    public MarioWindowInitializer(
        IWindow nativeWindow,
        MarioWindow marioWindow,
        ILogger<MarioWindow> logger,
        int width,
        int height)
    {
        _nativeWindow = nativeWindow;
        _marioWindow = marioWindow;
        _logger = logger;
        _width = width;
        _height = height;
    }

    /// <summary>
    /// Called when the native window loads. Creates the OpenGL context,
    /// logs window info, and subscribes to framebuffer resize events.
    /// </summary>
    public void HandleLoad()
    {
        _marioWindow.GLContext = _nativeWindow.CreateOpenGL();

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(
                Resources.Strings.Window_Opened,
                _width,
                _height,
                4,
                6);
        }

        _nativeWindow.FramebufferResize += HandleFramebufferResize;
    }

    /// <summary>
    /// Called when the framebuffer is resized. Logs the new size
    /// and notifies the MarioWindow resize event.
    /// </summary>
    /// <param name="size">New framebuffer size in pixels.</param>
    private void HandleFramebufferResize(Silk.NET.Maths.Vector2D<int> size)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug(Resources.Strings.Framebuffer_Resized, size.X, size.Y);
        }

        _marioWindow.RaiseResize(size.X, size.Y);
    }
}
