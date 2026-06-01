namespace MarioEngine.Desktop;

using System;
using System.IO;
using Silk.NET.OpenGL;

/// <summary>
/// Loads GLSL shader source code from files and compiles them into shader programs.
/// </summary>
internal static class ShaderLoader
{
    /// <summary>
    /// Loads vertex and fragment shaders from files and links them into a program.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="vertexPath">Path to the vertex shader source file.</param>
    /// <param name="fragmentPath">Path to the fragment shader source file.</param>
    /// <returns>Handle to the linked shader program.</returns>
    public static uint LoadProgram(GL gl, string vertexPath, string fragmentPath)
    {
        var vertexSource = File.ReadAllText(vertexPath);
        var fragmentSource = File.ReadAllText(fragmentPath);

        var vertex = CompileShader(gl, ShaderType.VertexShader, vertexSource);
        var fragment = CompileShader(gl, ShaderType.FragmentShader, fragmentSource);

        var program = gl.CreateProgram();
        gl.AttachShader(program, vertex);
        gl.AttachShader(program, fragment);
        gl.LinkProgram(program);

        gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out var success);
        if (success == 0)
        {
            var info = gl.GetProgramInfoLog(program);
            throw new InvalidOperationException($"Shader program link failed: {info}");
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
    /// <param name="type">Type of shader.</param>
    /// <param name="source">GLSL source code.</param>
    /// <returns>Handle to the compiled shader.</returns>
    private static uint CompileShader(GL gl, ShaderType type, string source)
    {
        var shader = gl.CreateShader(type);
        gl.ShaderSource(shader, source);
        gl.CompileShader(shader);

        gl.GetShader(shader, ShaderParameterName.CompileStatus, out var success);
        if (success == 0)
        {
            var info = gl.GetShaderInfoLog(shader);
            throw new InvalidOperationException($"Shader compile failed ({type}): {info}");
        }

        return shader;
    }
}
