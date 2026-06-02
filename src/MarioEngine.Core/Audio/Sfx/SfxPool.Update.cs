namespace MarioEngine.Core.Audio.Sfx;

using Silk.NET.OpenAL;

/// <summary>
/// Contains the update loop and disposal logic for the <see cref="SfxPool"/> class.
/// </summary>
public sealed partial class SfxPool
{
    /// <summary>
    /// Called every frame. Reclaims sources whose playback has finished.
    /// </summary>
    public void Update()
    {
        for (var i = 0; i < PoolSize; i++)
        {
            var instance = _instances[i];
            if (!instance.InUse)
            {
                continue;
            }

            int state;
            unsafe
            {
                _al.GetSourceProperty(instance.Source, GetSourceInteger.SourceState, &state);
            }

            if (state != (int)SourceState.Playing && state != (int)SourceState.Paused)
            {
                instance.InUse = false;
                _al.SourceStop(instance.Source);
                unsafe
                {
                    var queued = 0;
                    _al.GetSourceProperty(instance.Source, GetSourceInteger.BuffersQueued, &queued);
                    if (queued > 0)
                    {
                        fixed (uint* buf = _unqueueBuffer)
                        {
                            _al.SourceUnqueueBuffers(instance.Source, queued, buf);
                        }
                    }
                }
            }
        }
    }

    /// <summary>Releases all OpenAL sources.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        for (var i = 0; i < PoolSize; i++)
        {
            _instances[i].InUse = false;
            _al.SourceStop(_sources[i]);
            _al.DeleteSource(_sources[i]);
        }
    }
}
