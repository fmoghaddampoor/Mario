namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using System.IO;
using Silk.NET.OpenGL;

/// <summary>
/// Bloom post-processing pass: extracts bright pixels, applies Gaussian blur,
/// and combines the bloom with the original scene. Shaders loaded from files.
/// </summary>
public sealed class BloomPass : IDisposable
{
    private static readonly string ShaderDir = Path.Combine(AppContext.BaseDirectory, "Shaders");

    /// <summary>Shader program for bright-pass extraction.</summary>
    private readonly uint _brightProgram;

    /// <summary>Shader program for Gaussian blur (shared for H/V).</summary>
    private readonly uint _blurProgram;

    /// <summary>Shader program for bloom combine pass.</summary>
    private readonly uint _combineProgram;

    /// <summary>FBO for bright-pass and vertical blur results.</summary>
    private readonly FrameBuffer _brightFb;

    /// <summary>FBO for horizontal blur result.</summary>
    private readonly FrameBuffer _blurFb;

    /// <summary>True after Dispose has been called.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="BloomPass"/> class.</summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="width">Render width in pixels.</param>
    /// <param name="height">Render height in pixels.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public BloomPass(GL gl, int width, int height)
    {
        ArgumentNullException.ThrowIfNull(gl);
        _brightFb = new FrameBuffer(gl, width, height);
        _blurFb = new FrameBuffer(gl, width, height);

        var vert = File.ReadAllText(Path.Combine(ShaderDir, "fullscreen.vert"));
        _brightProgram = ShaderHelper.Compile(gl, vert, File.ReadAllText(Path.Combine(ShaderDir, "bloom_bright.frag")));
        _blurProgram = ShaderHelper.Compile(gl, vert, File.ReadAllText(Path.Combine(ShaderDir, "bloom_blur.frag")));
        _combineProgram = ShaderHelper.Compile(gl, vert, File.ReadAllText(Path.Combine(ShaderDir, "bloom_combine.frag")));
    }

    /// <summary>
    /// Applies the bloom effect to the input texture.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="inputTexture">Source scene texture handle.</param>
    /// <param name="outputFb">Target framebuffer. Must not be null.</param>
    /// <param name="threshold">Brightness threshold (0-1). Default 0.8.</param>
    /// <param name="intensity">Bloom intensity multiplier. Default 1.0.</param>
    public void Apply(GL gl, uint inputTexture, FrameBuffer outputFb, float threshold = 0.8f, float intensity = 1.0f)
    {
        ArgumentNullException.ThrowIfNull(gl);
        ArgumentNullException.ThrowIfNull(outputFb);
        var vao = ShaderHelper.QuadVao(gl);

        _brightFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_brightProgram);
        gl.Uniform1(gl.GetUniformLocation(_brightProgram, "uTexture"), 0);
        gl.Uniform1(gl.GetUniformLocation(_brightProgram, "uThreshold"), threshold);
        gl.ActiveTexture(TextureUnit.Texture0);
        gl.BindTexture(TextureTarget.Texture2D, inputTexture);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _blurFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_blurProgram);
        gl.Uniform1(gl.GetUniformLocation(_blurProgram, "uTexture"), 0);
        gl.Uniform2(gl.GetUniformLocation(_blurProgram, "uDirection"), 1f, 0f);
        gl.Uniform1(gl.GetUniformLocation(_blurProgram, "uSize"), 1f / _brightFb.Width);
        gl.BindTexture(TextureTarget.Texture2D, _brightFb.TextureHandle);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _brightFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_blurProgram);
        gl.Uniform2(gl.GetUniformLocation(_blurProgram, "uDirection"), 0f, 1f);
        gl.Uniform1(gl.GetUniformLocation(_blurProgram, "uSize"), 1f / _blurFb.Height);
        gl.BindTexture(TextureTarget.Texture2D, _blurFb.TextureHandle);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        outputFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_combineProgram);
        gl.Uniform1(gl.GetUniformLocation(_combineProgram, "uOriginal"), 0);
        gl.Uniform1(gl.GetUniformLocation(_combineProgram, "uBloom"), 1);
        gl.Uniform1(gl.GetUniformLocation(_combineProgram, "uIntensity"), intensity);
        gl.ActiveTexture(TextureUnit.Texture0);
        gl.BindTexture(TextureTarget.Texture2D, inputTexture);
        gl.ActiveTexture(TextureUnit.Texture1);
        gl.BindTexture(TextureTarget.Texture2D, _brightFb.TextureHandle);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        gl.BindVertexArray(0);
        gl.UseProgram(0);
    }

    /// <summary>Releases GPU resources.</summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _brightFb.Dispose();
            _blurFb.Dispose();
            _disposed = true;
        }
    }
}
