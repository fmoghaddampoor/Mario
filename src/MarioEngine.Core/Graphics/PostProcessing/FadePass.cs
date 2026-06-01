namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Fade-to-black (or from-black) transition pass.
/// Uses a uniform alpha value to control the fade level.
/// </summary>
public sealed class FadePass
{
    private const string FragSrc = @"
#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uAlpha;
void main() {
    vec4 color = texture(uTexture, vTexCoord);
    FragColor = vec4(color.rgb * (1.0 - uAlpha), 1.0);
}";

    private readonly uint _program;

    /// <summary>
    /// Initializes a new instance of the <see cref="FadePass"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public FadePass(GL gl)
    {
        ArgumentNullException.ThrowIfNull(gl);
        _program = ShaderHelper.Compile(gl, ShaderHelper.FullscreenVert, FragSrc);
    }

    /// <summary>
    /// Applies the fade effect to the input texture.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="inputTexture">Source scene texture.</param>
    /// <param name="outputFb">Target framebuffer. Must not be null.</param>
    /// <param name="alpha">Fade alpha (0 = visible, 1 = fully black).</param>
    /// <exception cref="ArgumentNullException">Thrown if gl or outputFb is null.</exception>
    public void Apply(GL gl, uint inputTexture, FrameBuffer outputFb, float alpha)
    {
        ArgumentNullException.ThrowIfNull(gl);
        ArgumentNullException.ThrowIfNull(outputFb);
        outputFb.Bind();
        gl.Clear(ClearBufferMask.ColorBufferBit);
        gl.UseProgram(_program);
        gl.ActiveTexture(TextureUnit.Texture0);
        gl.BindTexture(TextureTarget.Texture2D, inputTexture);
        gl.Uniform1(gl.GetUniformLocation(_program, "uTexture"), 0);
        gl.Uniform1(gl.GetUniformLocation(_program, "uAlpha"), alpha);
        gl.BindVertexArray(ShaderHelper.QuadVao(gl));
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        gl.BindVertexArray(0);
        gl.UseProgram(0);
    }
}
