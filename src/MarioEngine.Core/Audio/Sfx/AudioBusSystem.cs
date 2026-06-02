namespace MarioEngine.Core.Audio.Sfx;

using System;

/// <summary>
/// Manages per-bus volume multipliers and mute states.
/// Master volume is applied on top of individual bus volumes.
/// Bus volumes range from 0.0 (silent) to 1.0 (full).
/// </summary>
public sealed class AudioBusSystem
{
    /// <summary>Number of bus channels.</summary>
    private const int BusCount = 4;

    /// <summary>Volume multipliers per bus, indexed by <see cref="AudioBus"/>.</summary>
    private readonly float[] _volumes;

    /// <summary>Mute states per bus.</summary>
    private readonly bool[] _muted;

    /// <summary>Master volume multiplier applied to all buses.</summary>
    private float _masterVolume = 0.8f;

    /// <summary>Initializes a new instance of the <see cref="AudioBusSystem"/> class.</summary>
    public AudioBusSystem()
    {
        _volumes = new float[BusCount];
        _muted = new bool[BusCount];

        // Set defaults
        _volumes[(int)AudioBus.Master] = 0.8f;
        _volumes[(int)AudioBus.Music] = 0.7f;
        _volumes[(int)AudioBus.Sfx] = 0.6f;
        _volumes[(int)AudioBus.Voice] = 1.0f;
    }

    /// <summary>Gets or sets the master volume (0.0 to 1.0).</summary>
    public float MasterVolume
    {
        get => _masterVolume;
        set => _masterVolume = Math.Clamp(value, 0f, 1f);
    }

    /// <summary>
    /// Gets or sets the volume for a specific bus.
    /// </summary>
    /// <param name="bus">The audio bus.</param>
    /// <returns>Volume from 0.0 to 1.0.</returns>
    public float this[AudioBus bus]
    {
        get => _volumes[(int)bus];
        set => _volumes[(int)bus] = Math.Clamp(value, 0f, 1f);
    }

    /// <summary>
    /// Gets or sets the mute state for a specific bus.
    /// </summary>
    /// <param name="bus">The audio bus.</param>
    /// <returns>True if muted.</returns>
    public bool IsMuted(AudioBus bus) => _muted[(int)bus];

    /// <summary>
    /// Sets the mute state for a bus.
    /// </summary>
    /// <param name="bus">The audio bus.</param>
    /// <param name="muted">True to mute.</param>
    public void SetMuted(AudioBus bus, bool muted) => _muted[(int)bus] = muted;

    /// <summary>
    /// Calculates the effective gain for a source on a given bus,
    /// factoring in master volume, bus volume, and mute state.
    /// </summary>
    /// <param name="bus">The audio bus.</param>
    /// <returns>Effective gain (0.0 if muted, or MasterVolume * BusVolume).</returns>
    public float GetEffectiveGain(AudioBus bus)
    {
        if (_muted[(int)bus])
        {
            return 0f;
        }

        return _masterVolume * _volumes[(int)bus];
    }
}
