namespace MarioEngine.Core.Audio.Music;

using System;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Silk.NET.OpenAL;

/// <summary>
/// Streams an MP3 file from disk through OpenAL using buffer queueing.
/// Supports looping, volume control, and real-time pausing.
/// Call <see cref="Update"/> every frame to refill processed buffers.
/// </summary>
public sealed partial class MusicTrack : IDisposable
{
    /// <summary>Number of streaming buffers to queue on the OpenAL source.</summary>
    private const int BufferCount = 4;

    /// <summary>Size of each streaming buffer in bytes of decoded PCM data.</summary>
    private const int BufferSize = 65536;

    /// <summary>OpenAL API instance.</summary>
    private readonly AL _al;

    /// <summary>Logger for streaming events.</summary>
    private readonly ILogger _logger;

    /// <summary>Streaming OpenAL source handle.</summary>
    private readonly uint _source;

    /// <summary>Pool of OpenAL buffer handles for the ring buffer.</summary>
    private readonly uint[] _buffers;

    /// <summary>MP3 file decoder.</summary>
    private Mp3FileReader? _reader;

    /// <summary>True if the stream is currently playing.</summary>
    private bool _playing;

    /// <summary>True if the stream should loop when reaching the end.</summary>
    private bool _looping;

    /// <summary>True after the stream has reached the end of the file (non-looping).</summary>
    private bool _finished;

    /// <summary>Current volume (0.0 to 1.0).</summary>
    private float _volume = 1f;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="MusicTrack"/> class.</summary>
    /// <param name="al">OpenAL API instance. Must not be null.</param>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if al or logger is null.</exception>
    public MusicTrack(AL al, ILogger logger)
    {
        _al = al ?? throw new ArgumentNullException(nameof(al));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _source = al.GenSource();
        _buffers = new uint[BufferCount];
        for (var i = 0; i < BufferCount; i++)
        {
            _buffers[i] = al.GenBuffer();
        }
    }

    /// <summary>Gets the duration of the loaded track in seconds, or 0 if no track loaded.</summary>
    public double Duration => _reader?.TotalTime.TotalSeconds ?? 0;

    /// <summary>Gets or sets the current playback position in seconds.</summary>
    public double Position
    {
        get => _reader?.CurrentTime.TotalSeconds ?? 0;
        set
        {
            if (_reader != null)
            {
                _reader.CurrentTime = TimeSpan.FromSeconds(value);
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether the track loops when reaching the end.</summary>
    public bool Looping
    {
        get => _looping;
        set => _looping = value;
    }

    /// <summary>Gets or sets the volume (0.0 to 1.0).</summary>
    public float Volume
    {
        get => _volume;
        set
        {
            _volume = Math.Clamp(value, 0f, 1f);
            _al.SetSourceProperty(_source, SourceFloat.Gain, _volume);
        }
    }

    /// <summary>Gets a value indicating whether the stream is currently playing.</summary>
    public bool IsPlaying => _playing;

    /// <summary>Gets a value indicating whether the stream has finished (non-looping).</summary>
    public bool IsFinished => _finished;
}
