namespace MarioEngine.Desktop;

/// <summary>
/// Shared mutable state for the splash-to-game transition.
/// Used by <see cref="MarioWindowUpdateHandler"/> and <see cref="MarioWindowRenderHandler"/>.
/// </summary>
internal sealed class GameStartupState
{
    /// <summary>Gets or sets the splash screen. Null once the splash finishes.</summary>
    public SplashScreen? Splash { get; set; }

    /// <summary>Gets or sets a value indicating whether the game has started.</summary>
    public bool GameStarted { get; set; }
}
