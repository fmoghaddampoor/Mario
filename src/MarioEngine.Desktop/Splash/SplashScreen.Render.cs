namespace MarioEngine.Desktop;

using Silk.NET.OpenGL;

/// <summary>
/// Renders splash layers in order: rotating nebula → static stars → static overlay.
/// Nebula uses animated shader with mild UV drift; stars and overlay use simple texture pass.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>
    /// Renders all splash layers: rotating nebula, static stars, and text overlay.
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

        var texLoc = _gl.GetUniformLocation(_program, "uTexture");
        var timeLoc = _gl.GetUniformLocation(_program, "uTime");

        // Layer 1: Nebula (rotating)
        _gl.Viewport(vpX, vpY, (uint)vpW, (uint)vpH);
        _gl.UseProgram(_program);
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _nebulaTexture);
        _gl.Uniform1(texLoc, 0);
        _gl.Uniform1(timeLoc, _elapsed);
        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Layer 2: Stars (static, with blending)
        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        _gl.BindTexture(TextureTarget.Texture2D, _starsTexture);
        _gl.Uniform1(texLoc, 0);
        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        // Layer 3: Overlay (orb + text, static)
        _gl.BindTexture(TextureTarget.Texture2D, _overlayTexture);
        _gl.Uniform1(texLoc, 0);
        _gl.BindVertexArray(_vao);
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 6);

        _gl.BindVertexArray(0);
        _gl.UseProgram(0);
        _gl.Disable(EnableCap.Blend);
    }
}
