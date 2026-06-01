namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Bloom post-processing pass: extracts bright pixels, applies Gaussian blur,
/// and combines the bloom with the original scene.
/// </summary>
public sealed class BloomPass : IDisposable
{
    private const string BrightFrag = @"
#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uThreshold;
void main() {
    vec4 color = texture(uTexture, vTexCoord);
    float lum = dot(color.rgb, vec3(0.299, 0.587, 0.114));
    float brightness = max(0.0, lum - uThreshold);
    FragColor = color * brightness / max(lum, 0.001);
}";

    private const string BlurFrag = @"
#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform vec2 uDirection;
uniform float uSize;
void main() {
    vec4 color = texture(uTexture, vTexCoord) * 0.227;
    vec2 off = uDirection * uSize;
    color += texture(uTexture, vTexCoord + off * 1.0) * 0.194;
    color += texture(uTexture, vTexCoord - off * 1.0) * 0.194;
    color += texture(uTexture, vTexCoord + off * 2.0) * 0.121;
    color += texture(uTexture, vTexCoord - off * 2.0) * 0.121;
    color += texture(uTexture, vTexCoord + off * 3.0) * 0.054;
    color += texture(uTexture, vTexCoord - off * 3.0) * 0.054;
    FragColor = color;
}";

    private const string CombineFrag = @"
#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uOriginal;
uniform sampler2D uBloom;
uniform float uIntensity;
void main() {
    vec4 original = texture(uOriginal, vTexCoord);
    vec4 bloom = texture(uBloom, vTexCoord);
    FragColor = original + bloom * uIntensity;
}";

    private readonly uint _brightProgram;
    private readonly uint _blurProgram;
    private readonly uint _combineProgram;
    private readonly FrameBuffer _brightFb;
    private readonly FrameBuffer _blurFb;
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
        _brightProgram = ShaderHelper.Compile(gl, ShaderHelper.FullscreenVert, BrightFrag);
        _blurProgram = ShaderHelper.Compile(gl, ShaderHelper.FullscreenVert, BlurFrag);
        _combineProgram = ShaderHelper.Compile(gl, ShaderHelper.FullscreenVert, CombineFrag);
    }

    /// <summary>
    /// Applies the bloom effect to the input texture.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="inputTexture">Source scene texture handle.</param>
    /// <param name="outputFb">Target framebuffer for the result.</param>
    /// <param name="threshold">Brightness threshold (0-1). Default 0.8.</param>
    /// <param name="intensity">Bloom intensity multiplier. Default 1.0.</param>
    public void Apply(GL gl, uint inputTexture, FrameBuffer outputFb, float threshold = 0.8f, float intensity = 1.0f)
    {
        ArgumentNullException.ThrowIfNull(gl);
        ArgumentNullException.ThrowIfNull(outputFb);
        var vao = ShaderHelper.QuadVao(gl);

        // Bright pass
        _brightFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_brightProgram);
        gl.Uniform1(gl.GetUniformLocation(_brightProgram, "uTexture"), 0);
        gl.Uniform1(gl.GetUniformLocation(_brightProgram, "uThreshold"), threshold);
        gl.ActiveTexture(TextureUnit.Texture0);
        gl.BindTexture(TextureTarget.Texture2D, inputTexture);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Blur horizontal
        _blurFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_blurProgram);
        gl.Uniform1(gl.GetUniformLocation(_blurProgram, "uTexture"), 0);
        gl.Uniform2(gl.GetUniformLocation(_blurProgram, "uDirection"), 1f, 0f);
        gl.Uniform1(gl.GetUniformLocation(_blurProgram, "uSize"), 1f / _brightFb.Width);
        gl.BindTexture(TextureTarget.Texture2D, _brightFb.TextureHandle);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Blur vertical
        _brightFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_blurProgram);
        gl.Uniform2(gl.GetUniformLocation(_blurProgram, "uDirection"), 0f, 1f);
        gl.Uniform1(gl.GetUniformLocation(_blurProgram, "uSize"), 1f / _blurFb.Height);
        gl.BindTexture(TextureTarget.Texture2D, _blurFb.TextureHandle);
        gl.BindVertexArray(vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Combine
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
