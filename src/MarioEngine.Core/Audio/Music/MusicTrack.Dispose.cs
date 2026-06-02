namespace MarioEngine.Core.Audio.Music;

using Silk.NET.OpenAL;

/// <summary>
/// Contains the <see cref="Dispose"/> method for the <see cref="MusicTrack"/> class.
/// Releases all OpenAL sources, buffers, and the MP3 decoder.
/// </summary>
public sealed partial class MusicTrack
{
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
}
