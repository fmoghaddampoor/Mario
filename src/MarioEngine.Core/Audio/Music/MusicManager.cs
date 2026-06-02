namespace MarioEngine.Core.Audio.Music;

using System;
using System.IO;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Contains the <see cref="MusicManager"/> class declaration, fields, constructor,
/// and public properties.
/// </summary>
public sealed partial class MusicManager : IDisposable
{
    /// <summary>Base directory for music assets relative to the executable.</summary>
    private const string MusicBasePath = "assets/audio/music";

    /// <summary>Maximum number of concurrent stem tracks for layered music.</summary>
    private const int MaxStems = 4;

    /// <summary>OpenAL API instance.</summary>
    private readonly AL _al;

    /// <summary>Logger for music events.</summary>
    private readonly ILogger _logger;

    /// <summary>Active stem tracks for layered music.</summary>
    private readonly MusicTrack?[] _stems = new MusicTrack?[MaxStems];

    /// <summary>Currently active music track. Null when no music is playing.</summary>
    private MusicTrack? _current;

    /// <summary>Next track queued for crossfade transition. Null when idle.</summary>
    private MusicTrack? _next;

    /// <summary>Duration of the current crossfade transition in seconds.</summary>
    private float _crossfadeDuration;

    /// <summary>Elapsed time of the current crossfade in seconds.</summary>
    private float _crossfadeElapsed;

    /// <summary>Target volume for ducking (0.0 to 1.0).</summary>
    private float _duckTarget = 1f;

    /// <summary>Duration of the duck effect in seconds.</summary>
    private float _duckDuration;

    /// <summary>Elapsed time since duck started in seconds.</summary>
    private float _duckElapsed;

    /// <summary>Original volume before ducking for restoration.</summary>
    private float _duckOriginalVolume = 1f;

    /// <summary>True while a duck is active.</summary>
    private bool _ducking;

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

    /// <summary>Stops the currently playing music and all stems.</summary>
    public void Stop()
    {
        _current?.Dispose();
        _current = null;
        _next?.Dispose();
        _next = null;
        _crossfadeDuration = 0f;
        StopStems();
    }

    /// <summary>Stops all playing stem tracks.</summary>
    public void StopStems()
    {
        for (var i = 0; i < MaxStems; i++)
        {
            _stems[i]?.Dispose();
            _stems[i] = null;
        }
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

    /// <summary>Loads a track and configures it. Returns null if file not found.</summary>
    private MusicTrack? LoadTrack(string name, bool loop)
    {
        var path = Path.Combine(AppContext.BaseDirectory, MusicBasePath, $"{name}.mp3");
        if (!File.Exists(path))
        {
            _logger.LogWarning(Resources.Strings.Music_NotFound, path);
            return null;
        }

        var track = new MusicTrack(_al, _logger);
        track.Load(path);
        track.Looping = loop;
        track.Volume = 1f;
        track.Play();

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(Resources.Strings.Music_Playing, name, track.Duration);
        }

        return track;
    }
}
