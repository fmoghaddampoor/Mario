namespace MarioEngine.Desktop;

using System;
using System.IO;
using MarioEngine.Desktop.Resources;
using Serilog;
using Silk.NET.OpenGL;

/// <summary>
/// Displays a splash screen using multiple render layers:
/// - Background layer (gradient + nebula + center orb)
/// - Star layer (GPU particle system with independent twinkling)
/// - Text layer ("GRAVITON WORKS" with glow)
/// Each layer is independent, enabling clean animation effects.
/// </summary>
internal sealed partial class SplashScreen : IDisposable
{
    private const float ImageAspectRatio = 1920f / 1080f;

    private static readonly string BgPath = Path.Combine(AppContext.BaseDirectory, "splash_bg.png");
    private static readonly string TextPath = Path.Combine(AppContext.BaseDirectory, "splash_text.png");

    /// <summary>OpenGL context used for rendering.</summary>
    private readonly GL _gl;

    /// <summary>Shader program for textured quad rendering.</summary>
    private readonly uint _program;

    /// <summary>Background layer texture handle (gradient, nebula, orb).</summary>
    private readonly uint _bgTexture;

    /// <summary>Text layer texture handle (GRAVITON WORKS with glow).</summary>
    private readonly uint _textTexture;

    /// <summary>Full-screen quad vertex array object (shared for both layers).</summary>
    private readonly uint _vao;

    /// <summary>Full-screen quad vertex buffer object (shared for both layers).</summary>
    private readonly uint _vbo;

    /// <summary>How long the splash screen is displayed in seconds.</summary>
    private readonly float _displayDuration;

    /// <summary>Elapsed time in seconds since the splash screen started displaying.</summary>
    private float _elapsed;

    private SplashScreen(GL gl, uint program, uint bgTexture, uint textTexture, uint vao, uint vbo, float displayDuration)
    {
        _gl = gl;
        _program = program;
        _bgTexture = bgTexture;
        _textTexture = textTexture;
        _vao = vao;
        _vbo = vbo;
        _displayDuration = displayDuration;
    }

    /// <summary>Gets a value indicating whether the splash duration has elapsed.</summary>
    public bool IsFinished => _elapsed >= _displayDuration;

    /// <summary>
    /// Creates a splash screen, loading background and text textures from disk.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="displayDuration">How long to show the splash in seconds.</param>
    /// <returns>A configured SplashScreen ready to render.</returns>
    public static SplashScreen Create(GL gl, float displayDuration = 3f)
    {
        var program = CreateShaderProgram(gl);
        var bgTexture = LoadTexture(gl, BgPath);
        var textTexture = LoadTexture(gl, TextPath);
        var (vao, vbo) = CreateQuad(gl);

        Log.Information(Resources.Strings.Splash_Created);

        return new SplashScreen(gl, program, bgTexture, textTexture, vao, vbo, displayDuration);
    }

    /// <summary>
    /// Updates the splash timer. Called every frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        _elapsed += dt;
    }

    /// <summary>Releases all OpenGL resources.</summary>
    public void Dispose()
    {
        _gl.DeleteProgram(_program);
        _gl.DeleteTexture(_bgTexture);
        _gl.DeleteTexture(_textTexture);
        _gl.DeleteVertexArray(_vao);
        _gl.DeleteBuffer(_vbo);

        if (_starsInitialized)
        {
            _gl.DeleteProgram(_starProgram);
            _gl.DeleteVertexArray(_starVao);
            _gl.DeleteBuffer(_starVbo);
        }
    }
}
