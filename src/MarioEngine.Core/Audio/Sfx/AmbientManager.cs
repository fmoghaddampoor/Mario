namespace MarioEngine.Core.Audio.Sfx;

using System;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Manages looping ambient sound effects (wind, water, cave drips, etc.).
/// Each ambient is a dedicated OpenAL source with a looping buffer.
/// </summary>
public sealed class AmbientManager : IDisposable
{
    /// <summary>Maximum number of concurrent ambient sounds.</summary>
    private const int MaxAmbients = 8;

    /// <summary>OpenAL API instance.</summary>
    private readonly AL _al;

    /// <summary>Logger for ambient events.</summary>
    private readonly ILogger _logger;

    /// <summary>Active ambient source handles.</summary>
    private readonly uint[] _sources;

    /// <summary>Currently loaded buffer for each ambient slot.</summary>
    private readonly SoundBuffer?[] _buffers;

    /// <summary>Whether each slot is active.</summary>
    private readonly bool[] _active;

    /// <summary>Reusable buffer array for unqueue operations.</summary>
    private readonly uint[] _unqueueBuffer;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="AmbientManager"/> class.</summary>
    /// <param name="al">OpenAL API instance. Must not be null.</param>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if al or logger is null.</exception>
    public AmbientManager(AL al, ILogger logger)
    {
        _al = al ?? throw new ArgumentNullException(nameof(al));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sources = new uint[MaxAmbients];
        _buffers = new SoundBuffer[MaxAmbients];
        _active = new bool[MaxAmbients];
        _unqueueBuffer = new uint[1];

        for (var i = 0; i < MaxAmbients; i++)
        {
            _sources[i] = al.GenSource();
            al.SetSourceProperty(_sources[i], SourceBoolean.Looping, true);
        }
    }

    /// <summary>
    /// Plays a looping ambient sound in an available slot.
    /// </summary>
    /// <param name="buffer">Sound buffer to loop. Must not be null.</param>
    /// <param name="volume">Volume level (0.0 to 1.0).</param>
    /// <returns>True if the ambient was started, false if all slots are full.</returns>
    /// <exception cref="ArgumentNullException">Thrown if buffer is null.</exception>
    public bool Play(SoundBuffer buffer, float volume = 0.5f)
    {
        ArgumentNullException.ThrowIfNull(buffer);

        for (var i = 0; i < MaxAmbients; i++)
        {
            if (_active[i])
            {
                continue;
            }

            _buffers[i] = buffer;
            _active[i] = true;
            buffer.AddRef();

            var gain = Math.Clamp(volume, 0f, 1f);
            _al.SetSourceProperty(_sources[i], SourceFloat.Gain, gain);
            _al.SourceStop(_sources[i]);

            var handle = buffer.Handle;
            unsafe
            {
                _al.SourceQueueBuffers(_sources[i], 1, &handle);
            }

            _al.SourcePlay(_sources[i]);
            return true;
        }

        _logger.LogWarning("All ambient slots full");
        return false;
    }

    /// <summary>Stops all ambient sounds.</summary>
    public void StopAll()
    {
        for (var i = 0; i < MaxAmbients; i++)
        {
            if (!_active[i])
            {
                continue;
            }

            _al.SourceStop(_sources[i]);
            unsafe
            {
                var queued = 0;
                _al.GetSourceProperty(_sources[i], GetSourceInteger.BuffersQueued, &queued);
                if (queued > 0)
                {
                    fixed (uint* buf = _unqueueBuffer)
                    {
                        _al.SourceUnqueueBuffers(_sources[i], queued, buf);
                    }
                }
            }

            _buffers[i]?.Release();
            _buffers[i] = null;
            _active[i] = false;
        }
    }

    /// <summary>Releases all ambient sources and buffers.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        StopAll();

        for (var i = 0; i < MaxAmbients; i++)
        {
            _al.DeleteSource(_sources[i]);
        }
    }
}
