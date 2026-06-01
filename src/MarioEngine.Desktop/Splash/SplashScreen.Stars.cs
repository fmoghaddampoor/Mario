namespace MarioEngine.Desktop;

using Silk.NET.OpenGL;

/// <summary>
/// Renders animated background stars as a particle layer over the splash image.
/// Each star independently twinkles with its own phase and speed.
/// </summary>
internal sealed partial class SplashScreen
{
    private const int StarCount = 300;
    private const string StarVertexSource = @"
#version 460
layout(location = 0) in vec2 aPosition;
uniform float uTime;
uniform float uTwinkle;
out float vBrightness;

void main()
{
    float phase = sin(aPosition.x * 137.0 + aPosition.y * 73.0) * 0.5 + 0.5;
    float speed = 1.5 + phase * 2.5;
    float twinkle = sin(uTime * speed + phase * 6.28) * 0.5 + 0.5;
    vBrightness = 0.3 + 0.7 * mix(0.3, 1.0, twinkle * uTwinkle);
    gl_Position = vec4(aPosition, 0.0, 1.0);
    gl_PointSize = 2.0 + phase * 2.0;
}";

    private const string StarFragmentSource = @"
#version 460
in float vBrightness;
out vec4 FragColor;
void main()
{
    vec2 circ = gl_PointCoord - vec2(0.5);
    float dist = dot(circ, circ);
    if (dist > 0.25) discard;
    float alpha = smoothstep(0.25, 0.0, dist) * vBrightness;
    FragColor = vec4(1.0, 1.0, 1.0, alpha);
}";

    private uint _starProgram;
    private uint _starVao;
    private uint _starVbo;
    private bool _starsInitialized;

    /// <summary>Creates a shader program from vertex and fragment source strings.</summary>
    private static uint CreateShaderProgram(GL gl, string vertexSource, string fragmentSource)
    {
        var vertex = gl.CreateShader(ShaderType.VertexShader);
        gl.ShaderSource(vertex, vertexSource);
        gl.CompileShader(vertex);

        var fragment = gl.CreateShader(ShaderType.FragmentShader);
        gl.ShaderSource(fragment, fragmentSource);
        gl.CompileShader(fragment);

        var program = gl.CreateProgram();
        gl.AttachShader(program, vertex);
        gl.AttachShader(program, fragment);
        gl.LinkProgram(program);

        gl.DetachShader(program, vertex);
        gl.DetachShader(program, fragment);
        gl.DeleteShader(vertex);
        gl.DeleteShader(fragment);

        return program;
    }

    /// <summary>Initializes the star particle system with random positions on the GPU.</summary>
    /// <param name="gl">OpenGL context.</param>
#pragma warning disable CA5394 // Random is for visual star placement, not security
    private void InitializeStars(GL gl)
    {
        var rand = new Random(42);
        var positions = new float[StarCount * 2];
        for (var i = 0; i < StarCount; i++)
        {
            positions[i * 2] = (float)((rand.NextDouble() * 2.0) - 1.0);
            positions[(i * 2) + 1] = (float)((rand.NextDouble() * 2.0) - 1.0);
        }

        _starProgram = CreateShaderProgram(gl, StarVertexSource, StarFragmentSource);

        _starVao = gl.GenVertexArray();
        _starVbo = gl.GenBuffer();

        gl.BindVertexArray(_starVao);
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, _starVbo);

        unsafe
        {
            fixed (float* ptr = positions)
            {
                gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(positions.Length * sizeof(float)), ptr, BufferUsageARB.StaticDraw);
            }
        }

        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

        gl.BindVertexArray(0);

        _starsInitialized = true;
    }
#pragma warning restore CA5394

    /// <summary>Renders the background star layer over the splash image.</summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="elapsed">Elapsed time in seconds for animation.</param>
    private void RenderStars(GL gl, float elapsed)
    {
        if (!_starsInitialized)
        {
            InitializeStars(gl);
        }

        gl.UseProgram(_starProgram);

        var timeLoc = gl.GetUniformLocation(_starProgram, "uTime");
        gl.Uniform1(timeLoc, elapsed);

        var twinkleLoc = gl.GetUniformLocation(_starProgram, "uTwinkle");
        gl.Uniform1(twinkleLoc, 1f);

        gl.BindVertexArray(_starVao);
        gl.DrawArrays(PrimitiveType.Points, 0, StarCount);

        gl.BindVertexArray(0);
        gl.UseProgram(0);
    }
}
