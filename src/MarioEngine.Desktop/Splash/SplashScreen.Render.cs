namespace MarioEngine.Desktop;

using Silk.NET.OpenGL;

/// <summary>
/// Contains the <see cref="SplashScreen.Render"/> method.
/// Renders splash layers in order: background, stars, text.
/// Each layer uses correct aspect ratio with letterboxing.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>
    /// Renders all splash layers: background, stars, and text.
    /// Centers the 16:9 image within the current framebuffer with letterboxing.
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

        // Layer 1: Background (gradient, nebula, orb)
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _bgTexture);
        var texLoc = _gl.GetUniformLocation(_program, "uTexture");
        _gl.Uniform1(texLoc, 0);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Layer 2: Stars (GPU particles)
        _gl.BindVertexArray(0);
        RenderStars(_gl, _elapsed, fbWidth, fbHeight);

        // Layer 3: Text (GRAVITON WORKS with glow, overlaid with blending)
        _gl.Viewport(vpX, vpY, (uint)vpW, (uint)vpH);
        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        _gl.UseProgram(_program);
        _gl.BindTexture(TextureTarget.Texture2D, _textTexture);
        _gl.Uniform1(texLoc, 0);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _gl.BindVertexArray(0);
        _gl.UseProgram(0);
        _gl.Disable(EnableCap.Blend);
    }
}
