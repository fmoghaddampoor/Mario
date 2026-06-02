namespace MarioEngine.Core.Audio.Music;

using System;
using System.IO;
using Silk.NET.OpenAL;

/// <summary>
/// Contains playback control methods for the <see cref="MusicManager"/> class.
/// </summary>
public sealed partial class MusicManager
{
    /// <summary>
    /// Plays a music track by name. Stops any currently playing track.
    /// The file is expected at assets/audio/music/{name}.mp3.
    /// </summary>
    /// <param name="name">Track name without extension (e.g. &quot;world_1_grassland&quot;).</param>
    /// <param name="loop">Whether to loop the track.</param>
    public void Play(string name, bool loop = true)
    {
        Stop();
        _current = LoadTrack(name, loop);
    }

    /// <summary>
    /// Crossfades from the current track to a new track over a duration.
    /// The current track fades out while the new track fades in.
    /// </summary>
    /// <param name="name">New track name without extension.</param>
    /// <param name="fadeDuration">Crossfade duration in seconds (default 2.0).</param>
    /// <param name="loop">Whether the new track should loop.</param>
    public void CrossfadeTo(string name, float fadeDuration = 2f, bool loop = true)
    {
        _next?.Dispose();
        _next = LoadTrack(name, loop);

        if (_next == null)
        {
            return;
        }

        _next.Volume = 0f;
        _next.Play();

        _crossfadeDuration = Math.Max(fadeDuration, 0.1f);
        _crossfadeElapsed = 0f;
    }

    /// <summary>
    /// Plays layered music stems. Each stem is a separate audio file played simultaneously.
    /// Stems follow the naming pattern: {baseName}_stem_{layer}.
    /// </summary>
    /// <param name="baseName">Base track name (e.g. &quot;world_1_grassland&quot;).</param>
    /// <param name="activeStems">Bitmask of which stems to play (bit 0 = rhythm, 1 = harmony, 2 = melody, 3 = intensity).</param>
    /// <param name="loop">Whether stems should loop.</param>
    public void PlayStems(string baseName, int activeStems, bool loop = true)
    {
        StopStems();

        var stemNames = new[] { "rhythm", "harmony", "melody", "intensity" };
        for (var i = 0; i < MaxStems; i++)
        {
            if ((activeStems & (1 << i)) == 0)
            {
                continue;
            }

            var stemPath = Path.Combine(AppContext.BaseDirectory, MusicBasePath, $"{baseName}_stem_{stemNames[i]}.mp3");
            if (!File.Exists(stemPath))
            {
                continue;
            }

            var track = new MusicTrack(_al, _logger);
            track.Load(stemPath);
            track.Looping = loop;
            track.Volume = 0.7f;
            track.Play();
            _stems[i] = track;
        }

        Play(baseName, loop);
    }

    /// <summary>
    /// Updates the active stem bitmask during playback (fades stems in/out).
    /// </summary>
    /// <param name="activeStems">New bitmask of active stems.</param>
    public void SetActiveStems(int activeStems)
    {
        for (var i = 0; i < MaxStems; i++)
        {
            var shouldPlay = (activeStems & (1 << i)) != 0;
            var stem = _stems[i];

            if (!shouldPlay && stem != null)
            {
                stem.Stop();
                stem.Dispose();
                _stems[i] = null;
            }
        }
    }

    /// <summary>
    /// Temporarily reduces music volume (ducking) for a duration.
    /// Used when important SFX play to ensure they are heard clearly.
    /// </summary>
    /// <param name="duration">Duration of the duck effect in seconds.</param>
    /// <param name="targetVolume">Target volume during duck (0.0 to 1.0, default 0.3).</param>
    public void Duck(float duration, float targetVolume = 0.3f)
    {
        if (_current == null)
        {
            return;
        }

        _duckOriginalVolume = _current.Volume;
        _duckTarget = Math.Clamp(targetVolume, 0f, 1f);
        _duckDuration = Math.Max(duration, 0.1f);
        _duckElapsed = 0f;
        _ducking = true;
        _current.Volume = _duckTarget;
    }
}
