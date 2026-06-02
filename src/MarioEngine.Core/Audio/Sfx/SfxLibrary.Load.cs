namespace MarioEngine.Core.Audio;

using System;
using System.IO;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Contains the <see cref="Load"/> method for the <see cref="SfxLibrary"/> class.
/// </summary>
public sealed partial class SfxLibrary
{
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
}
