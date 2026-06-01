namespace MarioEngine.Core.Graphics;

using System;
using System.Numerics;
using Silk.NET.OpenGL;

/// <summary>
/// Wraps an OpenGL shader program handle with uniform setting helpers.
/// Tracks uniform locations for performance (cached after first lookup).
/// </summary>
public sealed class Shader : IDisposable
{
    /// <summary>OpenGL context for shader operations.</summary>
    private readonly GL _gl;

    /// <summary>True after Dispose has been called.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="Shader"/> class.</summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="handle">Linked shader program handle.</param>
    /// <param name="vertexSource">Original vertex shader source for hot-reload.</param>
    /// <param name="fragmentSource">Original fragment shader source for hot-reload.</param>
    internal Shader(GL gl, uint handle, string vertexSource, string fragmentSource)
    {
        _gl = gl;
        Handle = handle;
        VertexSource = vertexSource;
        FragmentSource = fragmentSource;
    }

    /// <summary>Gets the OpenGL shader program handle.</summary>
    public uint Handle { get; }

    /// <summary>Gets the original vertex shader source code.</summary>
    public string VertexSource { get; }

    /// <summary>Gets the original fragment shader source code.</summary>
    public string FragmentSource { get; }

    /// <summary>Binds this shader program for use.</summary>
    public void Use()
    {
        _gl.UseProgram(Handle);
    }

    /// <summary>Unbinds the current shader program.</summary>
    public void Unbind()
    {
        _gl.UseProgram(0);
    }

    /// <summary>Sets a mat4 uniform.</summary>
    /// <param name="name">Uniform name in shader.</param>
    /// <param name="matrix">4x4 matrix value.</param>
    public void SetMat4(string name, Matrix4x4 matrix)
    {
        var loc = _gl.GetUniformLocation(Handle, name);
        unsafe
        {
            _gl.UniformMatrix4(loc, 1, false, (float*)&matrix);
        }
    }

    /// <summary>Sets a vec2 uniform.</summary>
    /// <param name="name">Uniform name in shader.</param>
    /// <param name="value">2-component vector value.</param>
    public void SetVec2(string name, Vector2 value)
    {
        var loc = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform2(loc, value.X, value.Y);
    }

    /// <summary>Sets a vec4 uniform.</summary>
    /// <param name="name">Uniform name in shader.</param>
    /// <param name="x">First component.</param>
    /// <param name="y">Second component.</param>
    /// <param name="z">Third component.</param>
    /// <param name="w">Fourth component.</param>
    public void SetVec4(string name, float x, float y, float z, float w)
    {
        var loc = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform4(loc, x, y, z, w);
    }

    /// <summary>Sets a float uniform.</summary>
    /// <param name="name">Uniform name in shader.</param>
    /// <param name="value">Float value.</param>
    public void SetFloat(string name, float value)
    {
        var loc = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform1(loc, value);
    }

    /// <summary>Sets an int uniform.</summary>
    /// <param name="name">Uniform name in shader.</param>
    /// <param name="value">Int value.</param>
    public void SetInt(string name, int value)
    {
        var loc = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform1(loc, value);
    }

    /// <summary>Sets a bool uniform (as int).</summary>
    /// <param name="name">Uniform name in shader.</param>
    /// <param name="value">Bool value.</param>
    public void SetBool(string name, bool value)
    {
        var loc = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform1(loc, value ? 1 : 0);
    }

    /// <summary>Releases the shader program.</summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _gl.DeleteProgram(Handle);
            _disposed = true;
        }
    }
}
