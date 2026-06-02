namespace MarioEngine.Core.Audio.Music;

using System;
using System.IO;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Silk.NET.OpenAL;

/// <summary>
/// Streams an MP3 file from disk through OpenAL using buffer queueing.
/// Supports looping, volume control, and real-time pausing.
/// Call <see cref="Update"/> every frame to refill processed buffers.
/// </summary>
public sealed class MusicTrack : IDisposable
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

    /// <summary>Starts or resumes playback. If finished, restarts from beginning.</summary>
    public void Play()
    {
        if (_reader == null || _finished)
        {
            return;
        }

        _playing = true;
        _al.SourcePlay(_source);
    }

    /// <summary>Pauses playback without rewinding.</summary>
    public void Pause()
    {
        _playing = false;
        _al.SourcePause(_source);
    }

    /// <summary>Resumes playback from current position.</summary>
    public void Resume()
    {
        if (_reader == null || _finished)
        {
            return;
        }

        _playing = true;
        _al.SourcePlay(_source);
    }

    /// <summary>Stops playback and empties the buffer queue.</summary>
    public void Stop()
    {
        _playing = false;
        _al.SourceStop(_source);

        unsafe
        {
            var queued = 0;
            _al.GetSourceProperty(_source, GetSourceInteger.BuffersQueued, &queued);
            if (queued > 0)
            {
                var unqueued = stackalloc uint[BufferCount];
                _al.SourceUnqueueBuffers(_source, queued, unqueued);
            }
        }

        if (_reader != null)
        {
            _reader.CurrentTime = TimeSpan.Zero;
        }
    }

    /// <summary>
    /// Called every frame. Refills processed buffers with decoded PCM data.
    /// Must be called regularly to keep the stream alive.
    /// </summary>
    public unsafe void Update()
    {
        if (!_playing || _reader == null)
        {
            return;
        }

        int sourceState;
        _al.GetSourceProperty(_source, GetSourceInteger.SourceState, &sourceState);
        if (sourceState == (int)SourceState.Stopped && _finished)
        {
            _playing = false;
            return;
        }

        int processed;
        _al.GetSourceProperty(_source, GetSourceInteger.BuffersProcessed, &processed);

        for (var i = 0; i < processed; i++)
        {
            uint buf;
            _al.SourceUnqueueBuffers(_source, 1, &buf);

            if (FillBuffer(buf))
            {
                _al.SourceQueueBuffers(_source, 1, &buf);
            }
        }

        _al.GetSourceProperty(_source, GetSourceInteger.SourceState, &sourceState);
        if (sourceState != (int)SourceState.Playing)
        {
            _al.SourcePlay(_source);
        }
    }

    /// <summary>Releases all OpenAL resources.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _playing = false;
        _al.SourceStop(_source);

        unsafe
        {
            var queued = 0;
            _al.GetSourceProperty(_source, GetSourceInteger.BuffersQueued, &queued);
            if (queued > 0)
            {
                var unqueued = stackalloc uint[BufferCount];
                _al.SourceUnqueueBuffers(_source, queued, unqueued);
            }
        }

        _al.DeleteSource(_source);
        foreach (var buf in _buffers)
        {
            _al.DeleteBuffer(buf);
        }

        _reader?.Dispose();
    }

    private unsafe bool FillBuffer(uint buffer)
    {
        if (_reader == null)
        {
            return false;
        }

        var pcm = new byte[BufferSize];
        var bytesRead = _reader.Read(pcm, 0, BufferSize);

        if (bytesRead <= 0)
        {
            if (_looping)
            {
                _reader.CurrentTime = TimeSpan.Zero;
                bytesRead = _reader.Read(pcm, 0, BufferSize);
                if (bytesRead <= 0)
                {
                    return false;
                }
            }
            else
            {
                _finished = true;
                return false;
            }
        }

        var format = _reader.WaveFormat.Channels == 1 ? BufferFormat.Mono16 : BufferFormat.Stereo16;
        fixed (byte* ptr = pcm)
        {
            _al.BufferData(buffer, format, ptr, bytesRead, _reader.WaveFormat.SampleRate);
        }

        return true;
    }
}
