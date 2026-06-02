namespace MarioEngine.Core.Audio.Sfx;

/// <summary>
/// Defines a named sound effect with configurable properties.
/// Used by the audio cue system for organized SFX playback.
/// Cues can be defined in data and played by name.
/// </summary>
public sealed class AudioCue
{
    /// <summary>Initializes a new instance of the <see cref="AudioCue"/> class.</summary>
    /// <param name="name">Unique cue name.</param>
    /// <param name="sfxName">SFX asset filename without extension.</param>
    /// <param name="bus">Audio bus to play on.</param>
    /// <param name="priority">Priority (higher = more important).</param>
    /// <param name="volume">Base volume (0.0 to 1.0).</param>
    /// <param name="pitchMin">Minimum pitch multiplier.</param>
    /// <param name="pitchMax">Maximum pitch multiplier.</param>
    public AudioCue(
        string name,
        string sfxName,
        AudioBus bus = AudioBus.Sfx,
        int priority = 0,
        float volume = 1f,
        float pitchMin = 0.9f,
        float pitchMax = 1.1f)
    {
        Name = name;
        SfxName = sfxName;
        Bus = bus;
        Priority = priority;
        Volume = volume;
        PitchMin = pitchMin;
        PitchMax = pitchMax;
    }

    /// <summary>Gets the unique name of this cue.</summary>
    public string Name { get; }

    /// <summary>Gets the SFX asset name (filename without extension).</summary>
    public string SfxName { get; }

    /// <summary>Gets the audio bus this cue plays on.</summary>
    public AudioBus Bus { get; }

    /// <summary>Gets the priority level (higher = more important).</summary>
    public int Priority { get; }

    /// <summary>Gets the minimum pitch variation.</summary>
    public float PitchMin { get; }

    /// <summary>Gets the maximum pitch variation.</summary>
    public float PitchMax { get; }

    /// <summary>Gets the base volume (0.0 to 1.0).</summary>
    public float Volume { get; }
}
