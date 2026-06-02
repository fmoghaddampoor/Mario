namespace MarioEngine.Core.Graphics.State;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Caches and tracks OpenGL state to minimize redundant state changes.
/// Only applies a state change if the requested value differs from the cached value.
/// Call <see cref="Reset"/> at the start of each frame.
/// </summary>
#pragma warning disable CA1812 // Instantiated by render pipeline
internal sealed class GlStateCache
#pragma warning restore CA1812
{
    /// <summary>OpenGL context.</summary>
    private readonly GL _gl;

    /// <summary>Cached currently bound texture.</summary>
    private uint _currentTexture;

    /// <summary>Cached currently bound VAO.</summary>
    private uint _currentVao;

    /// <summary>Cached currently bound shader program.</summary>
    private uint _currentProgram;

    /// <summary>Cached blend enable state.</summary>
    private bool _blendEnabled;

    /// <summary>Cached blend source factor.</summary>
    private BlendingFactor _blendSrc = BlendingFactor.One;

    /// <summary>Cached blend destination factor.</summary>
    private BlendingFactor _blendDst = BlendingFactor.Zero;

    /// <summary>Initializes a new instance of the <see cref="GlStateCache"/> class.</summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public GlStateCache(GL gl)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
    }

    /// <summary>Resets all cached state. Call at the start of each frame.</summary>
    public void Reset()
    {
        _currentTexture = 0;
        _currentVao = 0;
        _currentProgram = 0;
        _blendEnabled = false;
        _blendSrc = BlendingFactor.One;
        _blendDst = BlendingFactor.Zero;
    }

    /// <summary>
    /// Binds a texture, only if different from the currently cached texture.
    /// </summary>
    /// <param name="texture">OpenGL texture handle.</param>
    public void BindTexture(uint texture)
    {
        if (_currentTexture == texture)
        {
            return;
        }

        _currentTexture = texture;
        _gl.BindTexture(TextureTarget.Texture2D, texture);
    }

    /// <summary>
    /// Binds a VAO, only if different from the currently cached VAO.
    /// </summary>
    /// <param name="vao">OpenGL VAO handle.</param>
    public void BindVertexArray(uint vao)
    {
        if (_currentVao == vao)
        {
            return;
        }

        _currentVao = vao;
        _gl.BindVertexArray(vao);
    }

    /// <summary>
    /// Uses a shader program, only if different from the currently cached program.
    /// </summary>
    /// <param name="program">OpenGL shader program handle.</param>
    public void UseProgram(uint program)
    {
        if (_currentProgram == program)
        {
            return;
        }

        _currentProgram = program;
        _gl.UseProgram(program);
    }

    /// <summary>
    /// Enables or disables blending, only if changed.
    /// </summary>
    /// <param name="enabled">True to enable blending.</param>
    public void SetBlendEnabled(bool enabled)
    {
        if (_blendEnabled == enabled)
        {
            return;
        }

        _blendEnabled = enabled;

        if (enabled)
        {
            _gl.Enable(EnableCap.Blend);
        }
        else
        {
            _gl.Disable(EnableCap.Blend);
        }
    }

    /// <summary>
    /// Sets blend function, only if changed.
    /// </summary>
    /// <param name="src">Source blend factor.</param>
    /// <param name="dst">Destination blend factor.</param>
    public void SetBlendFunc(BlendingFactor src, BlendingFactor dst)
    {
        if (_blendSrc == src && _blendDst == dst)
        {
            return;
        }

        _blendSrc = src;
        _blendDst = dst;
        _gl.BlendFunc(src, dst);
    }
}
