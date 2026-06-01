namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using System.IO;
using Silk.NET.OpenGL;

/// <summary>
/// Color grading pass using a lookup table (LUT) texture.
/// Maps input colors through a 3D LUT for artistic color correction.
/// Shader loaded from file.
/// </summary>
public sealed class ColorGradingPass
{
    private static readonly string ShaderDir = Path.Combine(AppContext.BaseDirectory, "Shaders");

    /// <summary>Shader program for LUT-based color grading.</summary>
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
        var vert = File.ReadAllText(Path.Combine(ShaderDir, "fullscreen.vert"));
        var frag = File.ReadAllText(Path.Combine(ShaderDir, "colorgrading.frag"));
        _program = ShaderHelper.Compile(gl, vert, frag);
    }

    /// <summary>Gets the LUT texture handle.</summary>
    public uint LUTTexture { get; }

    /// <summary>
    /// Applies color grading to the input texture.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="inputTexture">Source scene texture.</param>
    /// <param name="outputFb">Target framebuffer. Must not be null.</param>
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
