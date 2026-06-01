namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Wraps an OpenGL framebuffer object (FBO) with a color texture attachment.
/// Used by post-processing passes for offscreen rendering.
/// </summary>
public sealed class FrameBuffer : IDisposable
{
    /// <summary>OpenGL context for GPU operations.</summary>
    private readonly GL _gl;

    /// <summary>OpenGL framebuffer handle.</summary>
    private readonly uint _fbo;

    /// <summary>Color attachment texture handle.</summary>
    private readonly uint _texture;

    /// <summary>True after Dispose has been called.</summary>
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameBuffer"/> class.
    /// Creates an FBO with a single RGBA8 color texture attachment.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="width">Framebuffer width in pixels.</param>
    /// <param name="height">Framebuffer height in pixels.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public FrameBuffer(GL gl, int width, int height)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        Width = width;
        Height = height;

        _fbo = gl.GenFramebuffer();
        gl.BindFramebuffer(FramebufferTarget.Framebuffer, _fbo);

        _texture = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2D, _texture);
        var zero = IntPtr.Zero;
        gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba8, (uint)width, (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, in zero);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        gl.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _texture, 0);

        var status = gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
        if (status != GLEnum.FramebufferComplete)
        {
            throw new InvalidOperationException($"Framebuffer incomplete: {status}");
        }

        gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        TextureHandle = _texture;
    }

    /// <summary>Gets the OpenGL texture handle attached to this FBO.</summary>
    public uint TextureHandle { get; }

    /// <summary>Gets the framebuffer width in pixels.</summary>
    public int Width { get; }

    /// <summary>Gets the framebuffer height in pixels.</summary>
    public int Height { get; }

    /// <summary>Binds this FBO for rendering.</summary>
    public void Bind()
    {
        _gl.BindFramebuffer(FramebufferTarget.Framebuffer, _fbo);
        _gl.Viewport(0, 0, (uint)Width, (uint)Height);
    }

    /// <summary>Unbinds the FBO, restoring default framebuffer (screen).</summary>
    public void Unbind()
    {
        _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    /// <summary>Releases GPU resources.</summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _gl.DeleteFramebuffer(_fbo);
            _gl.DeleteTexture(_texture);
            _disposed = true;
        }
    }
}
