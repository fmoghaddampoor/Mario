namespace MarioEngine.Core.Config;

/// <summary>
/// Audio configuration section.
/// </summary>
public sealed class AudioConfig
{
    /// <summary>Gets or sets the master volume (0.0 to 1.0). Default 0.8.</summary>
    public float MasterVolume { get; set; } = 0.8f;

    /// <summary>Gets or sets the music volume (0.0 to 1.0). Default 0.7.</summary>
    public float MusicVolume { get; set; } = 0.7f;

    /// <summary>Gets or sets the SFX volume (0.0 to 1.0). Default 0.6.</summary>
    public float SfxVolume { get; set; } = 0.6f;

    /// <summary>Gets or sets the Seq server URL for structured logging. Empty to disable.</summary>
    public string SeqUrl { get; set; } = string.Empty;

    /// <summary>Gets or sets the Grafana Loki URL. Empty to disable.</summary>
    public string LokiUrl { get; set; } = string.Empty;
}
