namespace MarioEngine.Desktop;

using System;
using System.IO;
using MarioEngine.Desktop.Resources;
using Serilog;
using Silk.NET.OpenGL;

/// <summary>
/// Displays a splash screen image for a fixed duration using OpenGL.
/// Shows a spectacular cosmic scene for a configurable duration at startup.
/// Renders with correct aspect ratio and letterboxing on any display.
/// This class is split across multiple files by feature area.
/// </summary>
internal sealed partial class SplashScreen : IDisposable
{
    private const float ImageAspectRatio = 1920f / 1080f;

    private static readonly string SplashPath = Path.Combine(
        AppContext.BaseDirectory, "splash.png");

    /// <summary>OpenGL context used for rendering the splash texture.</summary>
    private readonly GL _gl;

    /// <summary>Handle to the compiled shader program for textured quad rendering.</summary>
    private readonly uint _program;

    /// <summary>Handle to the OpenGL texture containing the splash image.</summary>
    private readonly uint _textureHandle;

    /// <summary>Handle to the vertex array object for the full-screen quad.</summary>
    private readonly uint _vao;

    /// <summary>Handle to the vertex buffer object for the quad vertex data.</summary>
    private readonly uint _vbo;

    /// <summary>How long the splash screen is displayed in seconds.</summary>
    private readonly float _displayDuration;

    /// <summary>Elapsed time in seconds since the splash screen started displaying.</summary>
    private float _elapsed;

    private SplashScreen(GL gl, uint program, uint textureHandle, uint vao, uint vbo, float displayDuration)
    {
        _gl = gl;
        _program = program;
        _textureHandle = textureHandle;
        _vao = vao;
        _vbo = vbo;
        _displayDuration = displayDuration;
    }

    /// <summary>Gets a value indicating whether the splash duration has elapsed.</summary>
    public bool IsFinished => _elapsed >= _displayDuration;

    /// <summary>
    /// Creates a splash screen from the splash image file.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="displayDuration">How long to show the splash in seconds.</param>
    /// <returns>A configured SplashScreen ready to render.</returns>
    public static SplashScreen Create(GL gl, float displayDuration = 3f)
    {
        var program = CreateShaderProgram(gl);
        var textureHandle = LoadTexture(gl, SplashPath);
        var (vao, vbo) = CreateQuad(gl);

        Log.Information(Resources.Strings.Splash_Created);

        return new SplashScreen(gl, program, textureHandle, vao, vbo, displayDuration);
    }

    /// <summary>
    /// Updates the splash timer. Called every frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        _elapsed += dt;
    }

    /// <summary>Releases OpenGL resources.</summary>
    public void Dispose()
    {
        _gl.DeleteProgram(_program);
        _gl.DeleteTexture(_textureHandle);
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
