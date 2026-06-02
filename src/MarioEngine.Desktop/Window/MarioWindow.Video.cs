namespace MarioEngine.Desktop;

using MarioEngine.Desktop.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.Windowing;

/// <summary>
/// Contains fullscreen toggle and VSync control methods for the <see cref="MarioWindow"/> class.
/// </summary>
internal sealed partial class MarioWindow
{
    /// <summary>Gets the underlying Silk.NET window instance.</summary>
    public IWindow NativeWindow => _window;

    /// <summary>Gets a value indicating whether the window is currently in fullscreen mode.</summary>
    public bool IsFullscreen => _window.WindowState == WindowState.Fullscreen;

    /// <summary>Gets a value indicating whether VSync is currently enabled.</summary>
    public bool IsVSyncEnabled => _window.VSync;

    /// <summary>
    /// Toggles between fullscreen and windowed mode.
    /// </summary>
    public void ToggleFullscreen()
    {
        _window.WindowState = _window.WindowState == WindowState.Fullscreen
            ? WindowState.Normal
            : WindowState.Fullscreen;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            var state = _window.WindowState == WindowState.Fullscreen
                ? Resources.Strings.Video_FullscreenOn
                : Resources.Strings.Video_FullscreenOff;
            _logger.LogInformation(state);
        }
    }

    /// <summary>
    /// Enables or disables vertical synchronization.
    /// </summary>
    /// <param name="enabled">True to enable VSync, false to disable.</param>
    public void SetVSync(bool enabled)
    {
        _window.VSync = enabled;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            var msg = enabled
                ? Resources.Strings.Video_VSyncOn
                : Resources.Strings.Video_VSyncOff;
            _logger.LogInformation(msg);
        }
    }
}
