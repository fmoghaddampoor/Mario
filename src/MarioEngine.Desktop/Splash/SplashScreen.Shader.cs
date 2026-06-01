namespace MarioEngine.Desktop;

using System;
using MarioEngine.Desktop.Resources;
using Silk.NET.OpenGL;

/// <summary>
/// Contains shader sources and compilation for the SplashScreen.
/// Nebula shader: applies subtle UV rotation over time.
/// Static shader: simple texture passthrough (no effects).
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
    vec2 uv = vTexCoord;
    // Subtle nebula drift: slow sine-based offset
    uv.x += sin(uTime * 0.15 + uv.y * 3.0) * 0.006;
    uv.y += cos(uTime * 0.12 + uv.x * 3.0) * 0.006;
    FragColor = texture(uTexture, uv);
}";

    private static uint CreateShaderProgram(GL gl)
    {
        return CreateShaderProgram(gl, VertexShaderSource, FragmentShaderSource);
    }

    private static uint CreateShaderProgram(GL gl, string vertexSource, string fragmentSource)
    {
        var vertex = gl.CreateShader(ShaderType.VertexShader);
        gl.ShaderSource(vertex, vertexSource);
        gl.CompileShader(vertex);

        var fragment = gl.CreateShader(ShaderType.FragmentShader);
        gl.ShaderSource(fragment, fragmentSource);
        gl.CompileShader(fragment);

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
}
