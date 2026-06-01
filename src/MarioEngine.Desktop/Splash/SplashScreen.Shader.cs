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

void main()
{
    vec4 color = texture(uTexture, vTexCoord);
    float lum = dot(color.rgb, vec3(0.299, 0.587, 0.114));

    // Stars twinkle: bright pixels (lum > 0.6) pulse with sine wave
    // Each pixel gets a unique phase from its screen position for natural randomness
    if (lum > 0.6)
    {
        float twinkle = 0.7 + 0.3 * sin(uTime * 3.0 + vTexCoord.x * 50.0 + vTexCoord.y * 73.0);
        color.rgb *= twinkle;

        // Fade in stars at the edge of visibility for a shimmering effect
        float shimmer = sin(uTime * 5.0 + vTexCoord.x * 100.0 + vTexCoord.y * 137.0);
        shimmer = clamp(shimmer * 0.5 + 0.5, 0.3, 1.0);
        color.rgb *= mix(1.0, shimmer, smoothstep(0.6, 0.9, lum));
    }
    // Nebula/mist (mid-range luminance) slowly pulses
    else if (lum > 0.15)
    {
        float pulse = 0.95 + 0.05 * sin(uTime * 0.5 + vTexCoord.x * 10.0 + vTexCoord.y * 7.0);
        color.rgb *= pulse;
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
