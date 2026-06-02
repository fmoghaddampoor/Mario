namespace MarioEngine.Core.Config;

/// <summary>
/// Gameplay configuration section.
/// </summary>
public sealed class GameplayConfig
{
    /// <summary>Gets or sets the splash screen display duration in seconds. Default 3.</summary>
    public float SplashDuration { get; set; } = 3f;

    /// <summary>Gets or sets the difficulty preset: Easy, Normal, Hard. Default Normal.</summary>
    public string Difficulty { get; set; } = "Normal";

    /// <summary>Gets or sets the number of starting lives. Default 3.</summary>
    public int StartingLives { get; set; } = 3;

    /// <summary>Gets or sets the language code (e.g. "en", "de", "ja"). Default "en".</summary>
    public string Language { get; set; } = "en";

    /// <summary>Gets or sets a value indicating whether to show the FPS counter. Default false.</summary>
    public bool ShowFps { get; set; } = false;
}
