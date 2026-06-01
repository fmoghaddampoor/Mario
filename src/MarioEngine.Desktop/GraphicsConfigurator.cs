namespace MarioEngine.Desktop;

using Silk.NET.Windowing;

/// <summary>
/// Configures Silk.NET window options for the game.
/// Sets up OpenGL 4.6 core profile with vsync and fullscreen defaults.
/// </summary>
internal static class GraphicsConfigurator
{
    /// <summary>
    /// Applies graphics and window configuration to the given options.
    /// </summary>
    /// <param name="options">The window options to configure.</param>
    /// <param name="fullscreen">Whether to start in fullscreen mode.</param>
    public static void Configure(WindowOptions options, bool fullscreen)
    {
        options.WindowBorder = fullscreen ? WindowBorder.Hidden : WindowBorder.Resizable;
        options.VSync = true;
        options.API = new GraphicsAPI(
            ContextAPI.OpenGL,
            ContextProfile.Core,
            ContextFlags.ForwardCompatible,
            new APIVersion(4, 6));
    }
}
