namespace MarioEngine.Desktop;

using System;
using System.IO;
using Serilog;
using Silk.NET.OpenGL;
using StbImageSharp;

/// <summary>
/// Displays a splash screen image for a fixed duration using OpenGL.
/// Shows a spectacular cosmic scene for 3 seconds at startup.
/// </summary>
internal sealed class SplashScreen : IDisposable
{
    private const float DisplayDuration = 3f;
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
void main()
{
    FragColor = texture(uTexture, vTexCoord);
}";

    private static readonly string SplashPath = Path.Combine(
        AppContext.BaseDirectory, "splash.png");

    private readonly GL _gl;
    private readonly uint _program;
    private readonly uint _textureHandle;
    private readonly uint _vao;
    private readonly uint _vbo;
    private float _elapsed;

    private SplashScreen(GL gl, uint program, uint textureHandle, uint vao, uint vbo)
    {
        _gl = gl;
        _program = program;
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
        var program = CreateShaderProgram(gl);
        var textureHandle = LoadTexture(gl, SplashPath);
        var (vao, vbo) = CreateQuad(gl);

        Log.Information("Splash screen created");

        return new SplashScreen(gl, program, textureHandle, vao, vbo);
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
    /// Renders the splash image as a full-screen quad using a shader program.
    /// </summary>
    public void Render()
    {
        _gl.ClearColor(0f, 0f, 0f, 1f);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        _gl.UseProgram(_program);

        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _textureHandle);

        var texLoc = _gl.GetUniformLocation(_program, "uTexture");
        _gl.Uniform1(texLoc, 0);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _gl.BindVertexArray(0);
        _gl.UseProgram(0);
    }

    /// <summary>Releases OpenGL resources.</summary>
    public void Dispose()
    {
        _gl.DeleteProgram(_program);
        _gl.DeleteTexture(_textureHandle);
        _gl.DeleteVertexArray(_vao);
        _gl.DeleteBuffer(_vbo);
    }

    private static uint CreateShaderProgram(GL gl)
    {
        var vertex = LoadShader(gl, ShaderType.VertexShader, VertexShaderSource);
        var fragment = LoadShader(gl, ShaderType.FragmentShader, FragmentShaderSource);

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

    private static uint LoadShader(GL gl, ShaderType type, string source)
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
