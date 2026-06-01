namespace MarioEngine.Core.Config;

/// <summary>
/// Root configuration class containing all game settings sections.
/// Serialized to/from JSON for persistence.
/// </summary>
public sealed class GameConfig
{
    /// <summary>Gets or sets the video/graphics configuration section.</summary>
    public VideoConfig Video { get; set; } = new VideoConfig();

    /// <summary>Gets or sets the audio configuration section.</summary>
    public AudioConfig Audio { get; set; } = new AudioConfig();

    /// <summary>Gets or sets the input configuration section.</summary>
    public InputConfig Input { get; set; } = new InputConfig();

    /// <summary>Gets or sets the gameplay configuration section.</summary>
    public GameplayConfig Gameplay { get; set; } = new GameplayConfig();
}
