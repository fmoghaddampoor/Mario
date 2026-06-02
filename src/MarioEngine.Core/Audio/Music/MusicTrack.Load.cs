namespace MarioEngine.Core.Audio.Music;

using System.IO;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Silk.NET.OpenAL;

/// <summary>
/// Contains the <see cref="Load"/> method for the <see cref="MusicTrack"/> class.
/// </summary>
public sealed partial class MusicTrack
{
    /// <summary>
    /// Loads an MP3 file for streaming. Does not start playback.
    /// Call <see cref="Play"/> to begin.
    /// </summary>
    /// <param name="filePath">Full path to the .mp3 file.</param>
    public void Load(string filePath)
    {
        Stop();
        _reader?.Dispose();

        if (!File.Exists(filePath))
        {
            _logger.LogWarning(Resources.Strings.Music_NotFound, filePath);
            _finished = true;
            return;
        }

        _reader = new Mp3FileReader(filePath);
        _finished = false;
        _al.SetSourceProperty(_source, SourceFloat.Gain, _volume);
    }
}
