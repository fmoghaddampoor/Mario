namespace MarioEngine.Desktop;

using Silk.NET.OpenGL;

/// <summary>
/// Contains quad creation methods for the <see cref="SplashScreen"/> class.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>
    /// Creates a full-screen quad with position and texture coordinate vertex data.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <returns>Tuple of (VAO handle, VBO handle).</returns>
    private static (uint Vao, uint Vbo) CreateQuad(GL gl)
    {
        var vertices = new float[]
        {
            -1f, -1f, 0f, 1f,
            1f, -1f, 1f, 1f,
            1f, 1f, 1f, 0f,
            -1f, -1f, 0f, 1f,
            1f, 1f, 1f, 0f,
            -1f, 1f, 0f, 0f,
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
