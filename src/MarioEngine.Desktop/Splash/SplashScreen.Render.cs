namespace MarioEngine.Desktop;

using Silk.NET.OpenGL;

/// <summary>
/// Contains the <see cref="SplashScreen.Render"/> method.
/// Renders the splash with correct aspect ratio and letterboxing.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>
    /// Renders the splash image with correct aspect ratio and letterboxing.
    /// Centers the 16:9 image within the current framebuffer, filling remaining
    /// space with black bars.
    /// </summary>
    /// <param name="fbWidth">Framebuffer width in pixels.</param>
    /// <param name="fbHeight">Framebuffer height in pixels.</param>
    public void Render(int fbWidth, int fbHeight)
    {
        _gl.ClearColor(0f, 0f, 0f, 1f);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        if (fbWidth < 1 || fbHeight < 1)
        {
            return;
        }

        var fbAspect = (float)fbWidth / fbHeight;
        int vpX, vpY, vpW, vpH;

        if (fbAspect > ImageAspectRatio)
        {
            vpH = fbHeight;
            vpW = (int)(fbHeight * ImageAspectRatio);
            vpX = (int)((fbWidth - vpW) / 2f);
            vpY = 0;
        }
        else
        {
            vpW = fbWidth;
            vpH = (int)(fbWidth / ImageAspectRatio);
            vpX = 0;
            vpY = (int)((fbHeight - vpH) / 2f);
        }

        _gl.Viewport(vpX, vpY, (uint)vpW, (uint)vpH);

        _gl.UseProgram(_program);

        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _textureHandle);

        var texLoc = _gl.GetUniformLocation(_program, "uTexture");
        _gl.Uniform1(texLoc, 0);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _gl.BindVertexArray(0);
        _gl.UseProgram(0);

        RenderStars(_gl, _elapsed);
    }
}
