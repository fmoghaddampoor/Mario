namespace MarioEngine.Core.Audio.Sfx;

using Silk.NET.OpenAL;

/// <summary>
/// Represents a single active sound effect instance playing on an OpenAL source.
/// Created by <see cref="SfxPool"/> and recycled when playback completes.
/// </summary>
public sealed class SfxInstance
{
    /// <summary>Backing for <see cref="Source"/>.</summary>
    private uint _source;

    /// <summary>Backing for <see cref="Buffer"/>.</summary>
    private SoundBuffer? _buffer;

    /// <summary>Backing for <see cref="Bus"/>.</summary>
    private AudioBus _bus;

    /// <summary>Backing for <see cref="Priority"/>.</summary>
    private int _priority;

    /// <summary>Backing for <see cref="InUse"/>.</summary>
    private bool _inUse;

    /// <summary>Gets or sets the OpenAL source handle assigned to this instance.</summary>
    internal uint Source
    {
        get => _source;
        set => _source = value;
    }

    /// <summary>Gets or sets the sound buffer to play.</summary>
    internal SoundBuffer? Buffer
    {
        get => _buffer;
        set => _buffer = value;
    }

    /// <summary>Gets or sets the audio bus this instance belongs to.</summary>
    internal AudioBus Bus
    {
        get => _bus;
        set => _bus = value;
    }

    /// <summary>Gets or sets the priority level (higher = more important).</summary>
    internal int Priority
    {
        get => _priority;
        set => _priority = value;
    }

    /// <summary>Gets or sets a value indicating whether this instance is actively playing.</summary>
    internal bool InUse
    {
        get => _inUse;
        set => _inUse = value;
    }

    /// <summary>Gets a value indicating whether playback has finished.</summary>
    public bool IsPlaying => InUse;

    /// <summary>Stops playback and returns the source to the pool.</summary>
    public void Stop()
    {
        InUse = false;
    }
}
