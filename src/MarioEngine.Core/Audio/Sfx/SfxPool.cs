namespace MarioEngine.Core.Audio.Sfx;

using System;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Manages a pool of OpenAL sources for sound effect playback.
/// Supports priority-based recycling when all sources are in use.
/// Call <see cref="Update"/> every frame to reclaim finished sources.
/// </summary>
public sealed class SfxPool : IDisposable
{
    /// <summary>Default number of pooled OpenAL sources.</summary>
    private const int PoolSize = 16;

    /// <summary>OpenAL API instance.</summary>
    private readonly AL _al;

    /// <summary>Bus system for computing effective gains.</summary>
    private readonly AudioBusSystem _busSystem;

    /// <summary>Pooled source handles.</summary>
    private readonly uint[] _sources;

    /// <summary>Active SFX instances wrapping each source.</summary>
    private readonly SfxInstance[] _instances;

    /// <summary>Reusable buffer for unqueue operations (avoids repeated stackalloc).</summary>
    private readonly uint[] _unqueueBuffer;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="SfxPool"/> class.</summary>
    /// <param name="al">OpenAL API instance. Must not be null.</param>
    /// <param name="busSystem">Audio bus system for gain calculation. Must not be null.</param>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if al, busSystem, or logger is null.</exception>
    public SfxPool(AL al, AudioBusSystem busSystem, ILogger logger)
    {
        _al = al ?? throw new ArgumentNullException(nameof(al));
        _busSystem = busSystem ?? throw new ArgumentNullException(nameof(busSystem));
        ArgumentNullException.ThrowIfNull(logger);

        _sources = new uint[PoolSize];
        _instances = new SfxInstance[PoolSize];
        _unqueueBuffer = new uint[1];

        for (var i = 0; i < PoolSize; i++)
        {
            _sources[i] = al.GenSource();
            _instances[i] = new SfxInstance { Source = _sources[i] };
        }
    }

    /// <summary>Gets the total number of sources in the pool.</summary>
    public static int TotalSources => PoolSize;

    /// <summary>Gets the number of currently active (in-use) sources.</summary>
    public int ActiveCount
    {
        get
        {
            var count = 0;
            for (var i = 0; i < PoolSize; i++)
            {
                if (_instances[i].InUse)
                {
                    count++;
                }
            }

            return count;
        }
    }

    /// <summary>
    /// Plays a sound buffer on an available source.
    /// If all sources are in use, the lowest-priority active sound is stopped.
    /// </summary>
    /// <param name="buffer">The sound buffer to play.</param>
    /// <param name="priority">Priority level (higher = more important, default 0).</param>
    /// <param name="bus">Audio bus the sound belongs to.</param>
    /// <returns>The SfxInstance, or null if all sources are busy with higher-priority sounds.</returns>
    public SfxInstance? Play(SoundBuffer buffer, int priority = 0, AudioBus bus = AudioBus.Sfx)
    {
        ArgumentNullException.ThrowIfNull(buffer);
        var index = FindAvailableSource(priority);
        if (index < 0)
        {
            return null;
        }

        var instance = _instances[index];
        instance.Buffer = buffer;
        instance.Priority = priority;
        instance.Bus = bus;
        instance.InUse = true;

        var gain = _busSystem.GetEffectiveGain(bus);
        _al.SetSourceProperty(instance.Source, SourceFloat.Gain, gain);
        _al.SourceStop(instance.Source);

        var handle = buffer.Handle;
        unsafe
        {
            _al.SourceQueueBuffers(instance.Source, 1, &handle);
        }

        _al.SourcePlay(instance.Source);

        return instance;
    }

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

    /// <summary>
    /// Finds an available source slot, or steals the lowest-priority one.
    /// </summary>
    /// <param name="newPriority">Priority of the incoming sound.</param>
    /// <returns>Index of the available slot, or -1 if none.</returns>
    private int FindAvailableSource(int newPriority)
    {
        var lowestPriIndex = -1;
        var lowestPri = int.MaxValue;

        for (var i = 0; i < PoolSize; i++)
        {
            if (!_instances[i].InUse)
            {
                return i;
            }

            if (_instances[i].Priority < lowestPri)
            {
                lowestPri = _instances[i].Priority;
                lowestPriIndex = i;
            }
        }

        if (lowestPriIndex >= 0 && lowestPri < newPriority)
        {
            _al.SourceStop(_sources[lowestPriIndex]);
            _instances[lowestPriIndex].InUse = false;
            return lowestPriIndex;
        }

        return -1;
    }
}
