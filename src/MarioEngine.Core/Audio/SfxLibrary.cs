namespace MarioEngine.Core.Audio;

using System;
using System.Collections.Generic;
using System.IO;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Loads, caches, and manages sound effect buffers from WAV files.
/// Files are expected in assets/sfx/ organized by category subdirectories.
/// Provides lookups by name (without extension or path) for simple access.
/// </summary>
public sealed class SfxLibrary
{
    /// <summary>Base directory for SFX assets relative to the executable.</summary>
    private const string SfxBasePath = "assets/sfx";

    /// <summary>OpenAL API instance for buffer operations.</summary>
    private readonly AL _al;

    /// <summary>Logger for load events and errors.</summary>
    private readonly ILogger _logger;

    /// <summary>Cached sound buffers keyed by name (e.g. &quot;player_jump&quot;).</summary>
    private readonly Dictionary<string, SoundBuffer> _cache;

    /// <summary>Initializes a new instance of the <see cref="SfxLibrary"/> class.</summary>
    /// <param name="al">OpenAL API instance for creating buffers.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if al or logger is null.</exception>
    public SfxLibrary(AL al, ILogger logger)
    {
        _al = al ?? throw new ArgumentNullException(nameof(al));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = new Dictionary<string, SoundBuffer>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>Gets the number of loaded sound buffers.</summary>
    public int LoadedCount => _cache.Count;

    /// <summary>
    /// Loads a WAV file and creates an OpenAL buffer. Caches the result.
    /// The name is derived from the filename without extension.
    /// </summary>
    /// <param name="filePath">Path to the .wav file relative to the base directory.</param>
    /// <returns>The loaded SoundBuffer.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    /// <exception cref="InvalidDataException">Thrown if the file is not valid PCM WAV.</exception>
    public SoundBuffer Load(string filePath)
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, SfxBasePath, filePath);
        var name = Path.GetFileNameWithoutExtension(filePath);

        if (_cache.TryGetValue(name, out var existing))
        {
            existing.AddRef();
            return existing;
        }

        var wav = WavLoader.Load(fullPath);
        var handle = _al.GenBuffer();

        var format = (wav.Channels, wav.BitsPerSample) switch
        {
            (1, 8) => BufferFormat.Mono8,
            (1, 16) => BufferFormat.Mono16,
            (2, 8) => BufferFormat.Stereo8,
            (2, 16) => BufferFormat.Stereo16,
            _ => throw new NotSupportedException($"Unsupported WAV format: {wav.Channels}ch/{wav.BitsPerSample}bit"),
        };

        unsafe
        {
            fixed (byte* ptr = wav.Samples)
            {
                _al.BufferData(handle, format, ptr, wav.Samples.Length, wav.SampleRate);
            }
        }

        var duration = (float)wav.Samples.Length / (wav.SampleRate * wav.Channels * (wav.BitsPerSample / 8));
        var buffer = new SoundBuffer(_al, handle, duration);
        _cache[name] = buffer;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(
                Resources.Strings.Sfx_Loaded,
                name,
                format,
                wav.SampleRate,
                duration);
        }

        return buffer;
    }

    /// <summary>
    /// Tries to get a previously loaded sound buffer by name.
    /// </summary>
    /// <param name="name">The sound name (e.g. &quot;player_jump&quot;).</param>
    /// <returns>The SoundBuffer, or null if not loaded.</returns>
    public SoundBuffer? Get(string name)
    {
        _cache.TryGetValue(name, out var buffer);
        return buffer;
    }

    /// <summary>
    /// Removes a sound buffer from the cache and releases it.
    /// </summary>
    /// <param name="name">The sound name to unload.</param>
    public void Unload(string name)
    {
        if (_cache.TryGetValue(name, out var buffer))
        {
            _cache.Remove(name);
            buffer.Release();
        }
    }

    /// <summary>
    /// Unloads all sound buffers from the cache.
    /// </summary>
    public void UnloadAll()
    {
        foreach (var buffer in _cache.Values)
        {
            buffer.Release();
        }

        _cache.Clear();
        _logger.LogInformation(Resources.Strings.Sfx_UnloadedAll);
    }
}
