namespace MarioEngine.Core.Audio;

using System;
using Silk.NET.OpenAL;

/// <summary>
/// Wraps an OpenAL buffer handle containing PCM audio data.
/// Reference-counted: multiple callers can share the same SoundBuffer.
/// Disposes the OpenAL buffer when the last reference is released.
/// </summary>
public sealed class SoundBuffer : IDisposable
{
    /// <summary>OpenAL API instance for buffer operations.</summary>
    private readonly AL _al;

    /// <summary>OpenAL buffer handle.</summary>
    private readonly uint _handle;

    /// <summary>Duration of the sound in seconds.</summary>
    private readonly float _duration;

    /// <summary>Current reference count. Starts at 1.</summary>
    private int _refCount = 1;

    /// <summary>True after the underlying OpenAL buffer has been deleted.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="SoundBuffer"/> class.</summary>
    /// <param name="al">OpenAL API instance.</param>
    /// <param name="handle">OpenAL buffer handle (generated externally).</param>
    /// <param name="duration">Duration of the audio in seconds.</param>
    internal SoundBuffer(AL al, uint handle, float duration)
    {
        _al = al;
        _handle = handle;
        _duration = duration;
    }

    /// <summary>Gets the OpenAL buffer handle.</summary>
    public uint Handle => _handle;

    /// <summary>Gets the audio duration in seconds.</summary>
    public float Duration => _duration;

    /// <summary>Gets the current reference count.</summary>
    public int ReferenceCount => _refCount;

    /// <summary>
    /// Adds a reference to this buffer. Call when another component starts using it.
    /// </summary>
    public void AddRef()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _refCount++;
    }

    /// <summary>
    /// Releases a reference. When the count reaches zero, the OpenAL buffer is deleted.
    /// </summary>
    public void Release()
    {
        if (_disposed)
        {
            return;
        }

        _refCount--;
        if (_refCount <= 0)
        {
            Dispose();
        }
    }

    /// <summary>Force-disposes the OpenAL buffer regardless of reference count.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _al.DeleteBuffer(_handle);
    }
}
