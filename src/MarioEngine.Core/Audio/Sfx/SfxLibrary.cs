namespace MarioEngine.Core.Audio;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Loads, caches, and manages sound effect buffers from WAV files.
/// Files are expected in assets/sfx/ organized by category subdirectories.
/// Provides lookups by name (without extension or path) for simple access.
/// </summary>
public sealed partial class SfxLibrary
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
    /// Tries to get a previously loaded sound buffer by name.
    /// </summary>
    /// <param name="name">The sound name (e.g. &quot;player_jump&quot;).</param>
    /// <returns>The SoundBuffer, or null if not loaded.</returns>
    public SoundBuffer? Get(string name)
    {
        _cache.TryGetValue(name, out var buffer);
        return buffer;
    }
}
