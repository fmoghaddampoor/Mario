namespace MarioEngine.Desktop;

using System;
using MarioEngine.Desktop.Resources;
using Silk.NET.OpenGL;

/// <summary>
/// Contains shader compilation methods for the <see cref="SplashScreen"/> class.
/// </summary>
internal sealed partial class SplashScreen
{
    private const string VertexShaderSource = @"
#version 460
layout(location = 0) in vec2 aPosition;
layout(location = 1) in vec2 aTexCoord;
out vec2 vTexCoord;
void main()
{
    gl_Position = vec4(aPosition, 0.0, 1.0);
    vTexCoord = aTexCoord;
}";

    private const string FragmentShaderSource = @"
#version 460
in vec2 vTexCoord;
out vec4 FragColor;
uniform sampler2D uTexture;
uniform float uTime;
uniform float uTwinkle;

void main()
{
    vec4 color = texture(uTexture, vTexCoord);

    // Only very bright pixels (stars, not nebula) get a subtle twinkle
    float lum = dot(color.rgb, vec3(0.299, 0.587, 0.114));
    if (lum > 0.75)
    {
        float phase = sin(vTexCoord.x * 137.0 + vTexCoord.y * 73.0) * 0.5 + 0.5;
        float twinkle = 0.88 + 0.12 * sin(uTime * (2.0 + phase * 3.0) + phase * 6.28);
        color.rgb *= mix(1.0, twinkle, uTwinkle);
    }

    FragColor = color;
}";

    /// <summary>
    /// Creates a shader program by compiling vertex and fragment shaders and linking them.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <returns>Handle to the compiled and linked shader program.</returns>
    private static uint CreateShaderProgram(GL gl)
    {
        var vertex = LoadShader(gl, ShaderType.VertexShader, VertexShaderSource);
        var fragment = LoadShader(gl, ShaderType.FragmentShader, FragmentShaderSource);

        var program = gl.CreateProgram();
        gl.AttachShader(program, vertex);
        gl.AttachShader(program, fragment);
        gl.LinkProgram(program);

        gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out var success);
        if (success == 0)
        {
            var info = gl.GetProgramInfoLog(program);
            throw new InvalidOperationException(string.Format(Resources.Strings.Shader_LinkFailed, info));
        }

        gl.DetachShader(program, vertex);
        gl.DetachShader(program, fragment);
        gl.DeleteShader(vertex);
        gl.DeleteShader(fragment);

        return program;
    }

    /// <summary>
    /// Compiles a single shader from source code.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="type">Type of shader to compile.</param>
    /// <param name="source">GLSL source code.</param>
    /// <returns>Handle to the compiled shader.</returns>
    private static uint LoadShader(GL gl, ShaderType type, string source)
    {
        var shader = gl.CreateShader(type);
        gl.ShaderSource(shader, source);
        gl.CompileShader(shader);

        gl.GetShader(shader, ShaderParameterName.CompileStatus, out var success);
        if (success == 0)
        {
            var info = gl.GetShaderInfoLog(shader);
            throw new InvalidOperationException(string.Format(Resources.Strings.Shader_CompileFailed, type, info));
        }

        return shader;
    }
}
