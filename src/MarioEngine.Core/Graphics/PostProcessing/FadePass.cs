namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using System.IO;
using Silk.NET.OpenGL;

/// <summary>
/// Fade-to-black (or from-black) transition pass.
/// Uses a uniform alpha value to control the fade level.
/// Shader loaded from file.
/// </summary>
public sealed class FadePass
{
    private static readonly string ShaderDir = Path.Combine(AppContext.BaseDirectory, "Shaders");

    /// <summary>Shader program for fade-to-black effect.</summary>
    private readonly uint _program;

    /// <summary>
    /// Initializes a new instance of the <see cref="FadePass"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public FadePass(GL gl)
    {
        ArgumentNullException.ThrowIfNull(gl);
        var vert = File.ReadAllText(Path.Combine(ShaderDir, "fullscreen.vert"));
        var frag = File.ReadAllText(Path.Combine(ShaderDir, "fade.frag"));
        _program = ShaderHelper.Compile(gl, vert, frag);
    }

    /// <summary>
    /// Applies the fade effect to the input texture.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="inputTexture">Source scene texture.</param>
    /// <param name="outputFb">Target framebuffer. Must not be null.</param>
    /// <param name="alpha">Fade alpha (0 = visible, 1 = fully black).</param>
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
