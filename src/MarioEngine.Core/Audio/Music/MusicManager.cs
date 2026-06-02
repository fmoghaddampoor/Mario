namespace MarioEngine.Core.Audio.Music;

using System;
using System.IO;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Manages music track streaming, crossfade, and volume control.
/// Delegates per-track streaming to <see cref="MusicTrack"/> instances.
/// Call <see cref="Update"/> every frame to keep active streams filled.
/// </summary>
public sealed class MusicManager : IDisposable
{
    /// <summary>Base directory for music assets relative to the executable.</summary>
    private const string MusicBasePath = "assets/audio/music";

    /// <summary>OpenAL API instance.</summary>
    private readonly AL _al;

    /// <summary>Logger for music events.</summary>
    private readonly ILogger _logger;

    /// <summary>Currently active music track. Null when no music is playing.</summary>
    private MusicTrack? _current;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="MusicManager"/> class.</summary>
    /// <param name="al">OpenAL API instance. Must not be null.</param>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if al or logger is null.</exception>
    public MusicManager(AL al, ILogger logger)
    {
        _al = al ?? throw new ArgumentNullException(nameof(al));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>Gets a value indicating whether music is currently playing.</summary>
    public bool IsPlaying => _current?.IsPlaying ?? false;

    /// <summary>
    /// Plays a music track by name. Stops any currently playing track.
    /// The file is expected at assets/audio/music/{name}.mp3.
    /// </summary>
    /// <param name="name">Track name without extension (e.g. &quot;world_1_grassland&quot;).</param>
    /// <param name="loop">Whether to loop the track.</param>
    public void Play(string name, bool loop = true)
    {
        Stop();

        var path = Path.Combine(AppContext.BaseDirectory, MusicBasePath, $"{name}.mp3");
        if (!File.Exists(path))
        {
            _logger.LogWarning(Resources.Strings.Music_NotFound, path);
            return;
        }

        var track = new MusicTrack(_al, _logger);
        track.Load(path);
        track.Looping = loop;
        track.Volume = 1f;
        track.Play();
        _current = track;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(Resources.Strings.Music_Playing, name, track.Duration);
        }
    }

    /// <summary>Stops the currently playing music.</summary>
    public void Stop()
    {
        _current?.Dispose();
        _current = null;
    }

    /// <summary>Pauses the currently playing music.</summary>
    public void Pause()
    {
        _current?.Pause();
    }

    /// <summary>Resumes the currently playing music.</summary>
    public void Resume()
    {
        _current?.Resume();
    }

    /// <summary>
    /// Called every frame. Refills streaming buffers on the active track.
    /// </summary>
    public void Update()
    {
        _current?.Update();
    }

    /// <summary>Releases all music streams.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        Stop();
    }
}
