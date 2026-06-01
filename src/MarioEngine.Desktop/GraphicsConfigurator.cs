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
    /// <param name="parsed">Parsed CLI arguments with width, height, and fullscreen flag.</param>
    public static void Configure(WindowOptions options, (bool Fullscreen, int Width, int Height) parsed)
    {
        options.Title = Resources.Strings.Window_Title + VersionInfo.Current;
        options.Size = new Silk.NET.Maths.Vector2D<int>(parsed.Width, parsed.Height);
        options.WindowBorder = WindowBorder.Resizable;
        options.WindowState = parsed.Fullscreen ? WindowState.Fullscreen : WindowState.Normal;
        options.VSync = true;
        options.API = new GraphicsAPI(
            ContextAPI.OpenGL,
            ContextProfile.Core,
            ContextFlags.ForwardCompatible,
            new APIVersion(4, 6));
    }
}
