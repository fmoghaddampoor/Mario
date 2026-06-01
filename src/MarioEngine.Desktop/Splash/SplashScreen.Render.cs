namespace MarioEngine.Desktop;

using Silk.NET.OpenGL;

/// <summary>
/// Contains the <see cref="SplashScreen.Render"/> method.
/// Renders splash layers in order: background → animated stars → text.
/// Background has subtle nebula rotation; stars pulse with time.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>
    /// Renders all splash layers: rotating nebula background,
    /// pulsing star field, and static text overlay.
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

        // Layer 1: Rotating nebula background
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _bgTexture);
        var texLoc = _gl.GetUniformLocation(_program, "uTexture");
        _gl.Uniform1(texLoc, 0);
        var timeLoc = _gl.GetUniformLocation(_program, "uTime");
        _gl.Uniform1(timeLoc, _elapsed);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Layer 2: Pulsing stars (rendered with blending)
        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        _gl.BindTexture(TextureTarget.Texture2D, _starTexture);
        _gl.Uniform1(texLoc, 0);

        // Stars pulse: use a faster time multiplier for visible twinkle
        var starTime = _elapsed * 2.5f;
        _gl.Uniform1(timeLoc, starTime);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Layer 3: Text overlay
        _gl.BindTexture(TextureTarget.Texture2D, _textTexture);
        _gl.Uniform1(texLoc, 0);

        // Text doesn't animate, but the shader applies uTime anyway (no visible effect on solid white)
        _gl.Uniform1(timeLoc, 0f);

        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _gl.BindVertexArray(0);
        _gl.UseProgram(0);
        _gl.Disable(EnableCap.Blend);
    }
}
