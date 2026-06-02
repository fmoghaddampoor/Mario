namespace MarioEngine.Core.Audio;

using System;
using MarioEngine.Core.Config;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Manages the OpenAL audio device and context lifecycle using the high-level
/// <see cref="AudioContext"/> wrapper. Provides the AL API instance, volume control,
/// and graceful silent fallback if OpenAL is unavailable.
/// Call <see cref="Initialize"/> after the game starts, <see cref="Dispose"/> on shutdown.
/// </summary>
public sealed partial class AudioManager : IDisposable
{
    /// <summary>Logger for audio lifecycle events.</summary>
    private readonly ILogger _logger;

    /// <summary>Audio configuration for volume levels.</summary>
    private readonly AudioConfig _config;

    /// <summary>High-level OpenAL audio context wrapper. Null in silent fallback mode.</summary>
    private AudioContext? _context;

    /// <summary>Low-level AL API instance for source/buffer operations. Null in silent mode.</summary>
    private AL? _al;

    /// <summary>Library of loaded sound effect buffers.</summary>
    private SfxLibrary? _sfx;

    /// <summary>True after successful initialization.</summary>
    private bool _initialized;

    /// <summary>True if the manager has been disposed.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="AudioManager"/> class.</summary>
    /// <param name="config">Audio configuration with volume settings.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if config or logger is null.</exception>
    public AudioManager(AudioConfig config, ILogger logger)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>Gets the low-level AL API instance. Null in silent fallback mode.</summary>
    public AL? AL => _al;

    /// <summary>Gets the master volume (0.0 to 1.0).</summary>
    public float MasterVolume => _config.MasterVolume;

    /// <summary>Gets the music bus volume (0.0 to 1.0).</summary>
    public float MusicVolume => _config.MusicVolume;

    /// <summary>Gets the SFX bus volume (0.0 to 1.0).</summary>
    public float SfxVolume => _config.SfxVolume;

    /// <summary>Gets the SFX library for loading and caching sound effect buffers.</summary>
    public SfxLibrary? Sfx => _sfx;

    /// <summary>Gets a value indicating whether OpenAL was initialized successfully.</summary>
    public bool IsInitialized => _initialized;

    /// <summary>
    /// Sets the master volume and applies it to the OpenAL listener.
    /// </summary>
    /// <param name="volume">Volume level from 0.0 (silent) to 1.0 (full).</param>
    public void SetMasterVolume(float volume)
    {
        _config.MasterVolume = Math.Clamp(volume, 0f, 1f);
        if (_initialized && _al != null)
        {
            _al.SetListenerProperty(ListenerFloat.Gain, _config.MasterVolume);
        }
    }
}
