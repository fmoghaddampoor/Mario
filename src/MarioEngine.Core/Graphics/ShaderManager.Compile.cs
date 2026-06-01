namespace MarioEngine.Core.Graphics;

using System;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// Contains shader compilation and linking methods for <see cref="ShaderManager"/>.
/// </summary>
public sealed partial class ShaderManager
{
    /// <summary>
    /// Compiles vertex and fragment shaders, links them into a program.
    /// </summary>
    /// <param name="name">Debug name for error messages.</param>
    /// <param name="vertSource">Vertex shader GLSL source code.</param>
    /// <param name="fragSource">Fragment shader GLSL source code.</param>
    /// <returns>A new Shader instance wrapping the linked program.</returns>
    /// <exception cref="InvalidOperationException">Thrown if compilation or linking fails.</exception>
    private Shader CompileAndLink(string name, string vertSource, string fragSource)
    {
        var vertex = CompileSingleShader(ShaderType.VertexShader, vertSource);
        if (vertex == 0)
        {
            throw new InvalidOperationException($"Failed to compile vertex shader: {name}");
        }

        var fragment = CompileSingleShader(ShaderType.FragmentShader, fragSource);
        if (fragment == 0)
        {
            _gl.DeleteShader(vertex);
            throw new InvalidOperationException($"Failed to compile fragment shader: {name}");
        }

        var program = _gl.CreateProgram();
        _gl.AttachShader(program, vertex);
        _gl.AttachShader(program, fragment);
        _gl.LinkProgram(program);

        _gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out var success);
        if (success == 0)
        {
            var info = _gl.GetProgramInfoLog(program);
            _gl.DeleteProgram(program);
            _gl.DeleteShader(vertex);
            _gl.DeleteShader(fragment);
            _logger.LogError("Shader link failed ({Name}): {Info}", name, info);
            throw new InvalidOperationException($"Shader link failed: {name}\n{info}");
        }

        _gl.DetachShader(program, vertex);
        _gl.DetachShader(program, fragment);
        _gl.DeleteShader(vertex);
        _gl.DeleteShader(fragment);

        return new Shader(_gl, program, vertSource, fragSource);
    }

    /// <summary>
    /// Compiles a single shader from source code.
    /// </summary>
    /// <param name="type">Type of shader (Vertex, Fragment, etc.).</param>
    /// <param name="source">GLSL source code.</param>
    /// <returns>OpenGL shader handle, or 0 on failure.</returns>
    private uint CompileSingleShader(ShaderType type, string source)
    {
        var shader = _gl.CreateShader(type);
        _gl.ShaderSource(shader, source);
        _gl.CompileShader(shader);

        _gl.GetShader(shader, ShaderParameterName.CompileStatus, out var success);
        if (success == 0)
        {
            var info = _gl.GetShaderInfoLog(shader);
            _logger.LogError("Shader compile error ({Type}): {Info}", type, info);
            _gl.DeleteShader(shader);
            return 0;
        }

        return shader;
    }
}
