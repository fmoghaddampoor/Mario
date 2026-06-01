namespace MarioEngine.Core.Graphics;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Wraps an OpenGL texture handle with metadata and reference counting.
/// Used by <see cref="TextureManager"/> for cached texture management.
/// </summary>
public sealed class Texture2D : IDisposable
{
    /// <summary>OpenGL context for texture operations.</summary>
    private readonly GL _gl;

    /// <summary>True after Dispose has been called.</summary>
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="Texture2D"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="handle">OpenGL texture handle.</param>
    /// <param name="width">Texture width in pixels.</param>
    /// <param name="height">Texture height in pixels.</param>
    /// <param name="path">Source file path for cache lookup.</param>
    internal Texture2D(GL gl, uint handle, int width, int height, string path)
    {
        _gl = gl;
        Handle = handle;
        Width = width;
        Height = height;
        Path = path;
    }

    /// <summary>Gets the OpenGL texture handle.</summary>
    public uint Handle { get; }

    /// <summary>Gets the texture width in pixels.</summary>
    public int Width { get; }

    /// <summary>Gets the texture height in pixels.</summary>
    public int Height { get; }

    /// <summary>Gets the source file path used for caching.</summary>
    public string Path { get; }

    /// <summary>Gets the current reference count. Textures with zero references may be unloaded.</summary>
    public int ReferenceCount { get; private set; } = 1;

    /// <summary>Gets the aspect ratio (width / height).</summary>
    public float AspectRatio => (float)Width / Height;

    /// <summary>
    /// Increments the reference count. Call when another system starts using this texture.
    /// </summary>
    public void AddRef()
    {
        ReferenceCount++;
    }

    /// <summary>
    /// Decrements the reference count. When it reaches zero, the texture can be unloaded.
    /// </summary>
    public void Release()
    {
        if (ReferenceCount > 0)
        {
            ReferenceCount--;
        }
    }

    /// <summary>Releases the OpenGL texture resource.</summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _gl.DeleteTexture(Handle);
            _disposed = true;
        }
    }
}
