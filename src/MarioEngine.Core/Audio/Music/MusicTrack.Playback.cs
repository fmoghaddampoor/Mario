namespace MarioEngine.Core.Audio.Music;

using Silk.NET.OpenAL;

/// <summary>
/// Contains playback control methods for the <see cref="MusicTrack"/> class.
/// </summary>
public sealed partial class MusicTrack
{
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
}
