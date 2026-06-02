namespace MarioEngine.Core.Config;

/// <summary>
/// Video/graphics configuration section.
/// </summary>
public sealed class VideoConfig
{
    /// <summary>Gets or sets the display width in pixels. Default 1920.</summary>
    public int Width { get; set; } = 1920;

    /// <summary>Gets or sets the display height in pixels. Default 1080.</summary>
    public int Height { get; set; } = 1080;

    /// <summary>Gets or sets a value indicating whether to start in fullscreen mode. Default true.</summary>
    public bool Fullscreen { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether vsync is enabled. Default true.</summary>
    public bool VSync { get; set; } = true;

    /// <summary>Gets or sets the target FPS cap. 0 = unlimited. Default 60.</summary>
    public int FpsCap { get; set; } = 60;

    /// <summary>Gets or sets the resolution scale (0.5, 0.75, 1.0). Default 1.0.</summary>
    public float ResolutionScale { get; set; } = 1f;

    /// <summary>Gets or sets the MSAA sample count (0, 2, 4, 8). Default 0.</summary>
    public int MSAASamples { get; set; } = 0;
}
