namespace MarioEngine.Desktop;

using System;
using System.IO;
using MarioEngine.Desktop.Resources;
using Serilog;
using Silk.NET.OpenGL;

/// <summary>
/// Displays a splash screen using 3 render layers:
/// 1. Nebula (gradient + clouds, with subtle rotation).
/// 2. Stars (tiny dots, static).
/// 3. Overlay (GRAVITON WORKS text with glow, static).
/// </summary>
internal sealed partial class SplashScreen : IDisposable
{
    private const float ImageAspectRatio = 1920f / 1080f;

    private static readonly string NebulaPath = Path.Combine(AppContext.BaseDirectory, "splash_nebula.png");
    private static readonly string StarsPath = Path.Combine(AppContext.BaseDirectory, "splash_stars.png");
    private static readonly string OverlayPath = Path.Combine(AppContext.BaseDirectory, "splash_overlay.png");

    /// <summary>OpenGL context used for rendering.</summary>
    private readonly GL _gl;

    /// <summary>Shader program with nebula rotation animation.</summary>
    private readonly uint _program;

    /// <summary>Nebula layer texture (gradient + clouds).</summary>
    private readonly uint _nebulaTexture;

    /// <summary>Star field texture (dots on transparent).</summary>
    private readonly uint _starsTexture;

    /// <summary>Text overlay texture (GRAVITON WORKS with glow).</summary>
    private readonly uint _overlayTexture;

    /// <summary>Full-screen quad vertex array object.</summary>
    private readonly uint _vao;

    /// <summary>Full-screen quad vertex buffer object.</summary>
    private readonly uint _vbo;

    /// <summary>How long the splash screen is displayed in seconds.</summary>
    private readonly float _displayDuration;

    /// <summary>Elapsed time in seconds since the splash screen started.</summary>
    private float _elapsed;

    /// <summary>Initializes a new instance of the <see cref="SplashScreen"/> class.</summary>
    private SplashScreen(GL gl, uint program, uint nebulaTex, uint starsTex, uint overlayTex, uint vao, uint vbo, float displayDuration)
    {
        _gl = gl;
        _program = program;
        _nebulaTexture = nebulaTex;
        _starsTexture = starsTex;
        _overlayTexture = overlayTex;
        _vao = vao;
        _vbo = vbo;
        _displayDuration = displayDuration;
    }

    /// <summary>Gets a value indicating whether the splash display duration has elapsed.</summary>
    public bool IsFinished => _elapsed >= _displayDuration;

    /// <summary>
    /// Creates the splash screen by loading all layer textures and compiling shaders.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="displayDuration">How long to display the splash in seconds.</param>
    /// <returns>A configured SplashScreen ready to render.</returns>
    public static SplashScreen Create(GL gl, float displayDuration = 3f)
    {
        var program = CreateShaderProgram(gl);
        var nebulaTex = LoadTexture(gl, NebulaPath);
        var starsTex = LoadTexture(gl, StarsPath);
        var overlayTex = LoadTexture(gl, OverlayPath);
        var (vao, vbo) = CreateQuad(gl);

        Log.Information(Resources.Strings.Splash_Created);

        return new SplashScreen(gl, program, nebulaTex, starsTex, overlayTex, vao, vbo, displayDuration);
    }

    /// <summary>Advances the splash timer by the given delta time.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        _elapsed += dt;
    }

    /// <summary>Releases all OpenGL resources (shaders, textures, buffers).</summary>
    public void Dispose()
    {
        _gl.DeleteProgram(_program);
        _gl.DeleteTexture(_nebulaTexture);
        _gl.DeleteTexture(_starsTexture);
        _gl.DeleteTexture(_overlayTexture);
        _gl.DeleteVertexArray(_vao);
        _gl.DeleteBuffer(_vbo);
    }
}
