namespace MarioEngine.Desktop;

using System;
using System.IO;
using Serilog;
using Silk.NET.OpenGL;
using StbImageSharp;

/// <summary>
/// Displays a splash screen image for a fixed duration using OpenGL.
/// Shows "Graviton Works" branding on a black background for 3 seconds at startup.
/// </summary>
internal sealed class SplashScreen : IDisposable
{
    private const float DisplayDuration = 3f;
    private static readonly string SplashPath = System.IO.Path.Combine(
        System.AppContext.BaseDirectory, "splash.png");

    private readonly GL _gl;
    private readonly uint _textureHandle;
    private readonly uint _vao;
    private readonly uint _vbo;
    private float _elapsed;

    private SplashScreen(GL gl, uint textureHandle, uint vao, uint vbo)
    {
        _gl = gl;
        _textureHandle = textureHandle;
        _vao = vao;
        _vbo = vbo;
    }

    /// <summary>Gets a value indicating whether the splash duration has elapsed.</summary>
    public bool IsFinished => _elapsed >= DisplayDuration;

    /// <summary>
    /// Creates a splash screen from the splash image file.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <returns>A configured SplashScreen ready to render.</returns>
    public static SplashScreen Create(GL gl)
    {
        var textureHandle = LoadTexture(gl, SplashPath);
        var (vao, vbo) = CreateQuad(gl);

        Log.Information("Splash screen created");

        return new SplashScreen(gl, textureHandle, vao, vbo);
    }

    /// <summary>
    /// Updates the splash timer. Called every frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        _elapsed += dt;
    }

    /// <summary>
    /// Renders the splash image as a full-screen quad.
    /// </summary>
    public void Render()
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.Enable(EnableCap.Texture2D);
        _gl.BindTexture(TextureTarget.Texture2D, _textureHandle);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _gl.BindVertexArray(0);
        _gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    /// <summary>Releases OpenGL resources.</summary>
    public void Dispose()
    {
        _gl.DeleteTexture(_textureHandle);
        _gl.DeleteVertexArray(_vao);
        _gl.DeleteBuffer(_vbo);
    }

    private static uint LoadTexture(GL gl, string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Splash image not found: {path}");
        }

        ImageResult image;
        using (var stream = File.OpenRead(path))
        {
            image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }

        var handle = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2D, handle);

        gl.TexImage2D(
            TextureTarget.Texture2D,
            0,
            InternalFormat.Rgba,
            (uint)image.Width,
            (uint)image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            image.Data);

        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        gl.BindTexture(TextureTarget.Texture2D, 0);

        return handle;
    }

    private static (uint Vao, uint Vbo) CreateQuad(GL gl)
    {
        var vertices = new float[]
        {
            -1f, -1f, 0f, 0f,
            1f, -1f, 1f, 0f,
            1f, 1f, 1f, 1f,
            -1f, -1f, 0f, 0f,
            1f, 1f, 1f, 1f,
            -1f, 1f, 0f, 1f,
        };

        var vao = gl.GenVertexArray();
        var vbo = gl.GenBuffer();

        gl.BindVertexArray(vao);
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);

        unsafe
        {
            fixed (float* ptr = vertices)
            {
                gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), ptr, BufferUsageARB.StaticDraw);
            }
        }

        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);

        gl.EnableVertexAttribArray(1);
        gl.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));

        gl.BindVertexArray(0);

        return (vao, vbo);
    }
}
