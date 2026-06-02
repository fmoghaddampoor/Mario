namespace MarioEngine.Core.Audio.Music;

using System;
using NAudio.Wave;
using Silk.NET.OpenAL;

/// <summary>
/// Contains the streaming update loop for the <see cref="MusicTrack"/> class.
/// Called every frame to refill processed OpenAL buffers with decoded PCM data.
/// </summary>
public sealed partial class MusicTrack
{
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

    /// <summary>
    /// Reads decoded PCM data from the MP3 reader and uploads it to an OpenAL buffer.
    /// Handles looping by seeking back to the start when the stream ends.
    /// </summary>
    /// <param name="buffer">OpenAL buffer handle to fill.</param>
    /// <returns>True if data was written, false if end of stream (non-looping).</returns>
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
