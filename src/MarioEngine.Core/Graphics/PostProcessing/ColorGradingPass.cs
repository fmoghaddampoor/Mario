namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Color grading pass using a lookup table (LUT) texture.
/// Maps input colors through a 3D LUT for artistic color correction.
/// </summary>
public sealed class ColorGradingPass
{
    private const string FragSrc = @"
#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform sampler2D uLUT;
void main() {
    vec4 color = texture(uTexture, vTexCoord);
    float index = color.r + color.g * 16.0 + color.b * 256.0;
    float lutX = mod(index, 16.0) / 16.0 + 0.5 / 256.0;
    float lutY = floor(index / 16.0) / 16.0 + 0.5 / 16.0;
    vec3 graded = texture(uLUT, vec2(lutX, lutY)).rgb;
    FragColor = vec4(graded, color.a);
}";

    private readonly uint _program;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorGradingPass"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="lutTexture">3D LUT texture handle (256x16 RGBA).</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public ColorGradingPass(GL gl, uint lutTexture)
    {
        ArgumentNullException.ThrowIfNull(gl);
        LUTTexture = lutTexture;
        _program = ShaderHelper.Compile(gl, ShaderHelper.FullscreenVert, FragSrc);
    }

    /// <summary>Gets the LUT texture handle.</summary>
    public uint LUTTexture { get; }

    /// <summary>
    /// Applies color grading to the input texture.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="inputTexture">Source scene texture.</param>
    /// <param name="outputFb">Target framebuffer. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl or outputFb is null.</exception>
    public void Apply(GL gl, uint inputTexture, FrameBuffer outputFb)
    {
        ArgumentNullException.ThrowIfNull(gl);
        ArgumentNullException.ThrowIfNull(outputFb);
        outputFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_program);
        gl.ActiveTexture(TextureUnit.Texture0);
        gl.BindTexture(TextureTarget.Texture2D, inputTexture);
        gl.ActiveTexture(TextureUnit.Texture1);
        gl.BindTexture(TextureTarget.Texture2D, LUTTexture);
        gl.Uniform1(gl.GetUniformLocation(_program, "uTexture"), 0);
        gl.Uniform1(gl.GetUniformLocation(_program, "uLUT"), 1);
        gl.BindVertexArray(ShaderHelper.QuadVao(gl));
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        gl.BindVertexArray(0);
        gl.UseProgram(0);
    }
}
