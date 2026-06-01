namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Shared shader compilation and quad rendering utilities for post-processing passes.
/// </summary>
internal static class ShaderHelper
{
    /// <summary>Standard full-screen quad vertex shader shared by all post-processing passes.</summary>
    internal const string FullscreenVert = @"
#version 460
layout(location = 0) in vec2 aPosition;
layout(location = 1) in vec2 aTexCoord;
out vec2 vTexCoord;
void main() {
    gl_Position = vec4(aPosition, 0.0, 1.0);
    vTexCoord = aTexCoord;
}";

    /// <summary>Cached full-screen quad VAO handle.</summary>
    private static uint _vao;

    /// <summary>True after the shared VAO has been created.</summary>
    private static bool _vaoInitialized;

    /// <summary>
    /// Gets a shared full-screen quad VAO, creating it on first access.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <returns>Vertex array object handle for a full-screen quad.</returns>
    internal static uint QuadVao(GL gl)
    {
        if (!_vaoInitialized)
        {
            _vao = gl.GenVertexArray();
            var vbo = gl.GenBuffer();
            gl.BindVertexArray(_vao);
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
            float[] verts =
            {
                -1, -1, 0, 0,   1, -1, 1, 0,
                1,  1, 1, 1,  -1, -1, 0, 0,
                1,  1, 1, 1,  -1,  1, 0, 1,
            };
            unsafe
            {
                fixed (float* p = verts)
                {
                    gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(verts.Length * 4), p, BufferUsageARB.StaticDraw);
                }
            }

            gl.EnableVertexAttribArray(0);
            gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 16, 0);
            gl.EnableVertexAttribArray(1);
            gl.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 16, 8);
            gl.BindVertexArray(0);
            _vaoInitialized = true;
        }

        return _vao;
    }

    /// <summary>
    /// Compiles a shader program from vertex and fragment source code.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="vertSrc">Vertex shader GLSL source.</param>
    /// <param name="fragSrc">Fragment shader GLSL source.</param>
    /// <returns>Handle to the compiled and linked shader program.</returns>
    internal static uint Compile(GL gl, string vertSrc, string fragSrc)
    {
        var vs = gl.CreateShader(ShaderType.VertexShader);
        gl.ShaderSource(vs, vertSrc);
        gl.CompileShader(vs);
        var fs = gl.CreateShader(ShaderType.FragmentShader);
        gl.ShaderSource(fs, fragSrc);
        gl.CompileShader(fs);
        var prog = gl.CreateProgram();
        gl.AttachShader(prog, vs);
        gl.AttachShader(prog, fs);
        gl.LinkProgram(prog);
        gl.DetachShader(prog, vs);
        gl.DetachShader(prog, fs);
        gl.DeleteShader(vs);
        gl.DeleteShader(fs);
        return prog;
    }
}
