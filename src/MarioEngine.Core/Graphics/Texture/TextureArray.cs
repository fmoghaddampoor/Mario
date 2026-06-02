namespace MarioEngine.Core.Graphics.Texture;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Manages a 2D texture array for terrain rendering.
/// Each layer stores a separate tile texture.
/// All layers share the same width and height.
/// </summary>
#pragma warning disable CA1812 // Instantiated by terrain renderer
internal sealed class TextureArray : IDisposable
#pragma warning restore CA1812
{
    /// <summary>OpenGL context.</summary>
    private readonly GL _gl;

    /// <summary>OpenGL texture array handle.</summary>
    private readonly uint _handle;

    /// <summary>Width of each layer in pixels.</summary>
    private readonly int _width;

    /// <summary>Height of each layer in pixels.</summary>
    private readonly int _height;

    /// <summary>Number of layers in the array.</summary>
    private readonly int _layers;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="TextureArray"/> class.</summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="width">Width of each layer in pixels.</param>
    /// <param name="height">Height of each layer in pixels.</param>
    /// <param name="layers">Number of layers.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public TextureArray(GL gl, int width, int height, int layers)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _width = width;
        _height = height;
        _layers = layers;
        _handle = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2DArray, _handle);
        gl.TexImage3D(TextureTarget.Texture2DArray, 0, InternalFormat.Rgba8, (uint)width, (uint)height, (uint)layers, 0, PixelFormat.Rgba, PixelType.UnsignedByte, in IntPtr.Zero);
        gl.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        gl.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        gl.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        gl.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        gl.BindTexture(TextureTarget.Texture2DArray, 0);
    }

    /// <summary>Gets the OpenGL texture array handle.</summary>
    public uint Handle => _handle;

    /// <summary>Gets the width of each layer in pixels.</summary>
    public int Width => _width;

    /// <summary>Gets the height of each layer in pixels.</summary>
    public int Height => _height;

    /// <summary>Gets the number of layers.</summary>
    public int Layers => _layers;

    /// <summary>
    /// Uploads pixel data for a specific layer.
    /// </summary>
    /// <param name="layer">Layer index to upload to.</param>
    /// <param name="pixels">RGBA pixel data (width * height * 4 bytes).</param>
    public unsafe void SetLayer(int layer, byte[] pixels)
    {
        _gl.BindTexture(TextureTarget.Texture2DArray, _handle);
        fixed (byte* ptr = pixels)
        {
            _gl.TexSubImage3D(TextureTarget.Texture2DArray, 0, 0, 0, layer, (uint)_width, (uint)_height, 1, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }

        _gl.BindTexture(TextureTarget.Texture2DArray, 0);
    }

    /// <summary>Releases the texture array.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _gl.DeleteTexture(_handle);
    }
}
