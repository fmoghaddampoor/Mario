namespace MarioEngine.Core.Graphics;

using System;
using System.Numerics;
using MarioEngine.Core.Graphics.Font;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// High-level 2D renderer wrapping <see cref="SpriteBatcher"/> with camera support.
/// Provides DrawTexture, DrawRect, DrawLine, DrawString methods.
/// Uses begin/end pattern — call Begin() once per frame, End() to flush.
/// </summary>
public sealed class Renderer2D
{
    /// <summary>Sprite batcher for submitting draw calls.</summary>
    private readonly SpriteBatcher _batcher;

    /// <summary>Logger for debug output and warnings.</summary>
    private readonly ILogger<Renderer2D> _logger;

    /// <summary>OpenGL context for generating the white placeholder texture.</summary>
    private readonly GL _gl;

    /// <summary>Handle of the generated 1x1 white texture for rect/line drawing.</summary>
    private uint _whiteTexture;

    /// <summary>
    /// Initializes a new instance of the <see cref="Renderer2D"/> class.
    /// </summary>
    /// <param name="batcher">The sprite batcher to submit draw calls to.</param>
    /// <param name="gl">OpenGL context for generating utility textures.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if batcher, gl, or logger is null.</exception>
    public Renderer2D(SpriteBatcher batcher, GL gl, ILogger<Renderer2D> logger)
    {
        _batcher = batcher ?? throw new ArgumentNullException(nameof(batcher));
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        GenerateWhiteTexture();
    }

    /// <summary>Gets the current camera used for world-to-clip space conversion.</summary>
    public Camera2D Camera { get; private set; } = new Camera2D();

    /// <summary>Gets or sets the bitmap font used for DrawString calls.</summary>
    public BitmapFont? Font { get; set; }

    /// <summary>Gets the generated 1x1 white texture handle for rect/line primitives.</summary>
    public uint WhiteTexture => _whiteTexture;

    /// <summary>
    /// Begins a new frame. Call once per frame before any draw methods.
    /// </summary>
    public void Begin()
    {
        _batcher.Begin();
    }

    /// <summary>
    /// Ends the frame. Flushes all batched sprites to the GPU.
    /// </summary>
    public void End()
    {
        _batcher.End();
    }

    /// <summary>
    /// Renders a textured quad at the specified world position.
    /// </summary>
    /// <param name="texture">OpenGL texture handle.</param>
    /// <param name="x">World X position in pixels (center of sprite).</param>
    /// <param name="y">World Y position in pixels (center of sprite).</param>
    /// <param name="width">Sprite width in pixels.</param>
    /// <param name="height">Sprite height in pixels.</param>
    /// <param name="color">RGBA tint color packed as uint (0xRRGGBBAA).</param>
    /// <param name="layer">Render layer (higher = on top).</param>
    /// <param name="flipX">Whether to flip horizontally.</param>
    /// <param name="flipY">Whether to flip vertically.</param>
    public void DrawTexture(
        uint texture,
        float x,
        float y,
        float width,
        float height,
        uint color,
        float layer,
        bool flipX = false,
        bool flipY = false)
    {
        var pos = Camera.WorldToClip(x - (width * 0.5f), y - (height * 0.5f));
        var size = Camera.WorldSizeToClip(width, height);

        var u1 = flipX ? 1f : 0f;
        var u2 = flipX ? 0f : 1f;
        var v1 = flipY ? 1f : 0f;
        var v2 = flipY ? 0f : 1f;

        _batcher.Draw(texture, pos.X, pos.Y, size.X, size.Y, color, layer, u1, v1, u2, v2);
    }

    /// <summary>Generates a 1x1 white pixel texture for rect and line drawing primitives.</summary>
    private void GenerateWhiteTexture()
    {
        _whiteTexture = _gl.GenTexture();
        _gl.BindTexture(TextureTarget.Texture2D, _whiteTexture);
        var pixel = new byte[] { 255, 255, 255, 255 };
        unsafe
        {
            fixed (byte* ptr = pixel)
            {
                _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, 1, 1, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }
        }

        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        _gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    /// <summary>
    /// Renders a textured quad from a region of a texture atlas.
    /// </summary>
    /// <param name="texture">OpenGL texture handle.</param>
    /// <param name="x">World X position in pixels (top-left).</param>
    /// <param name="y">World Y position in pixels (top-left).</param>
    /// <param name="width">Sprite width in pixels.</param>
    /// <param name="height">Sprite height in pixels.</param>
    /// <param name="color">RGBA tint color packed as uint.</param>
    /// <param name="layer">Render layer (higher = on top).</param>
    /// <param name="u1">Texture coordinate left (0-1).</param>
    /// <param name="v1">Texture coordinate top (0-1).</param>
    /// <param name="u2">Texture coordinate right (0-1).</param>
    /// <param name="v2">Texture coordinate bottom (0-1).</param>
    public void DrawTextureRegion(
        uint texture,
        float x,
        float y,
        float width,
        float height,
        uint color,
        float layer,
        float u1,
        float v1,
        float u2,
        float v2)
    {
        var pos = Camera.WorldToClip(x, y);
        var size = Camera.WorldSizeToClip(width, height);
        _batcher.Draw(texture, pos.X, pos.Y, size.X, size.Y, color, layer, u1, v1, u2, v2);
    }

    /// <summary>
    /// Renders a filled rectangle in screen-space (not affected by camera).
    /// Coordinates are relative to the top-left corner of the screen in pixels.
    /// </summary>
    /// <param name="x">Screen X position from left in pixels.</param>
    /// <param name="y">Screen Y position from top in pixels.</param>
    /// <param name="width">Rectangle width in pixels.</param>
    /// <param name="height">Rectangle height in pixels.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer (higher = on top).</param>
    public void DrawScreenRect(float x, float y, float width, float height, uint color, float layer)
    {
        var viewW = Camera.ViewportWidth;
        var viewH = Camera.ViewportHeight;
        var cx = ((x / viewW) * 2f) - 1f;
        var cy = 1f - ((y / viewH) * 2f);
        var cw = (width / viewW) * 2f;
        var ch2 = (height / viewH) * 2f;
        _batcher.Draw(_whiteTexture, cx, cy - ch2, cw, ch2, color, layer);
    }

    /// <summary>
    /// Renders a filled rectangle for debugging using the generated white texture.
    /// </summary>
    /// <param name="x">World X position (top-left) in pixels.</param>
    /// <param name="y">World Y position (top-left) in pixels.</param>
    /// <param name="width">Rectangle width in pixels.</param>
    /// <param name="height">Rectangle height in pixels.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer.</param>
    public void DrawRect(float x, float y, float width, float height, uint color, float layer)
    {
        var pos = Camera.WorldToClip(x, y);
        var size = Camera.WorldSizeToClip(width, height);
        _batcher.Draw(_whiteTexture, pos.X, pos.Y, size.X, size.Y, color, layer);
    }

    /// <summary>
    /// Renders a line between two points for debugging using the white texture.
    /// Draws a thin rotated rectangle spanning from start to end.
    /// </summary>
    /// <param name="x1">Start X in world pixels.</param>
    /// <param name="y1">Start Y in world pixels.</param>
    /// <param name="x2">End X in world pixels.</param>
    /// <param name="y2">End Y in world pixels.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer.</param>
    public void DrawLine(float x1, float y1, float x2, float y2, uint color, float layer)
    {
        var dx = x2 - x1;
        var dy = y2 - y1;
        var len = MathF.Sqrt((dx * dx) + (dy * dy));
        if (len < 0.01f)
        {
            return;
        }

        var halfW = Camera.ViewportWidth * 0.5f / Camera.Zoom;
        var halfH = Camera.ViewportHeight * 0.5f / Camera.Zoom;
        var cx = ((x1 + (dx * 0.5f) - Camera.Position.X) / halfW) + Camera.ShakeOffset.X;
        var cy = -(((y1 + (dy * 0.5f) - Camera.Position.Y) / halfH) + Camera.ShakeOffset.Y);
        var cw = len / halfW;
        var ch = 2f / halfH;
        _batcher.Draw(_whiteTexture, cx, cy, cw, ch, color, layer);
    }

    /// <summary>
    /// Renders a text string using the currently assigned bitmap font (world space).
    /// Characters are drawn left-to-right, newlines move down by LineHeight.
    /// </summary>
    /// <param name="text">String to render.</param>
    /// <param name="x">World X position in pixels (baseline left).</param>
    /// <param name="y">World Y position in pixels (baseline top).</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer (higher = on top).</param>
    public void DrawString(string text, float x, float y, uint color, float layer)
    {
        if (Font == null || string.IsNullOrEmpty(text))
        {
            return;
        }

        var font = Font;
        var startX = x;
        foreach (var c in text)
        {
            if (c == '\n')
            {
                x = startX;
                y += font.LineHeight;
                continue;
            }

            var ch = font.GetCharacter(c);
            if (ch == null)
            {
                continue;
            }

            var charX = x + ch.OffsetX;
            var charY = y + ch.OffsetY;
            var pos = Camera.WorldToClip(charX, charY);
            var size = Camera.WorldSizeToClip(ch.Width, ch.Height);
            var textureHandle = (uint)font.AtlasTextureHandle;
            _batcher.Draw(textureHandle, pos.X, pos.Y, size.X, size.Y, color, layer, ch.U1, ch.V1, ch.U2, ch.V2);
            x += ch.AdvanceX;
        }
    }

    /// <summary>
    /// Renders a text string in screen-space (not affected by camera position/zoom).
    /// Coordinates are relative to the top-left corner of the screen in pixels.
    /// </summary>
    /// <param name="text">String to render.</param>
    /// <param name="x">Screen X position in pixels from left.</param>
    /// <param name="y">Screen Y position in pixels from top.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer (higher = on top).</param>
    public void DrawStringScreen(string text, float x, float y, uint color, float layer)
    {
        if (Font == null || string.IsNullOrEmpty(text))
        {
            return;
        }

        var font = Font;
        var viewW = Camera.ViewportWidth;
        var viewH = Camera.ViewportHeight;
        var startX = x;
        foreach (var c in text)
        {
            if (c == '\n')
            {
                x = startX;
                y += font.LineHeight;
                continue;
            }

            var ch = font.GetCharacter(c);
            if (ch == null)
            {
                continue;
            }

            var charX = x + ch.OffsetX;
            var charY = y + ch.OffsetY;
            var cx = ((charX / viewW) * 2f) - 1f;
            var cy = 1f - ((charY / viewH) * 2f);
            var cw = (ch.Width / viewW) * 2f;
            var ch2 = (ch.Height / viewH) * 2f;
            var textureHandle = (uint)font.AtlasTextureHandle;
            _batcher.Draw(textureHandle, cx, cy - ch2, cw, ch2, color, layer, ch.U1, ch.V1, ch.U2, ch.V2);
            x += ch.AdvanceX;
        }
    }
}
