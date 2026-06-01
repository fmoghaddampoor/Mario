namespace MarioEngine.Desktop;

using MarioEngine.Core;
using MarioEngine.Desktop.Resources;
using Silk.NET.Windowing;

/// <summary>
/// Configures Silk.NET window options for the game.
/// Sets up window title, size, OpenGL 4.6 core profile, vsync, and fullscreen.
/// </summary>
internal static class GraphicsConfigurator
{
    /// <summary>
    /// Applies all graphics and window configuration to the given options.
    /// </summary>
    /// <param name="options">The window options to configure.</param>
    /// <param name="width">Window width in pixels.</param>
    /// <param name="height">Window height in pixels.</param>
    /// <param name="fullscreen">Whether to start in fullscreen mode.</param>
    public static void Configure(WindowOptions options, int width, int height, bool fullscreen)
    {
        options.Title = Resources.Strings.Window_Title + VersionInfo.Current;
        options.Size = new Silk.NET.Maths.Vector2D<int>(width, height);
        options.WindowBorder = fullscreen ? WindowBorder.Hidden : WindowBorder.Resizable;
        options.VSync = true;
        options.API = new GraphicsAPI(
            ContextAPI.OpenGL,
            ContextProfile.Core,
            ContextFlags.ForwardCompatible,
            new APIVersion(4, 6));
    }
}
