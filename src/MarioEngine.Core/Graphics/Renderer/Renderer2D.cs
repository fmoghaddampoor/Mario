namespace MarioEngine.Core.Graphics;

using System;
using System.Numerics;
using Microsoft.Extensions.Logging;

/// <summary>
/// High-level 2D renderer wrapping <see cref="SpriteBatcher"/> with camera support.
/// Provides DrawTexture, DrawRect, DrawLine methods.
/// Uses begin/end pattern — call Begin() once per frame, End() to flush.
/// </summary>
public sealed class Renderer2D
{
    /// <summary>Sprite batcher for submitting draw calls.</summary>
    private readonly SpriteBatcher _batcher;

    /// <summary>Logger for debug output and warnings.</summary>
    private readonly ILogger<Renderer2D> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="Renderer2D"/> class.
    /// </summary>
    /// <param name="batcher">The sprite batcher to submit draw calls to.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if batcher is null.</exception>
    public Renderer2D(SpriteBatcher batcher, ILogger<Renderer2D> logger)
    {
        _batcher = batcher ?? throw new ArgumentNullException(nameof(batcher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>Gets the current camera used for world-to-clip space conversion.</summary>
    public Camera2D Camera { get; private set; } = new Camera2D();

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
    /// Renders a filled rectangle for debugging. Requires a white placeholder texture.
    /// Stub — functional when TextureManager provides a white texture.
    /// </summary>
    /// <param name="x">World X position (top-left) in pixels.</param>
    /// <param name="y">World Y position (top-left) in pixels.</param>
    /// <param name="width">Rectangle width in pixels.</param>
    /// <param name="height">Rectangle height in pixels.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer.</param>
    public void DrawRect(float x, float y, float width, float height, uint color, float layer)
    {
        _logger.LogWarning("DrawRect not implemented — requires white placeholder texture");
    }

    /// <summary>
    /// Renders a line between two points for debugging.
    /// Stub — functional when TextureManager provides a white texture.
    /// </summary>
    /// <param name="x1">Start X in world pixels.</param>
    /// <param name="y1">Start Y in world pixels.</param>
    /// <param name="x2">End X in world pixels.</param>
    /// <param name="y2">End Y in world pixels.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer.</param>
    public void DrawLine(float x1, float y1, float x2, float y2, uint color, float layer)
    {
        _logger.LogWarning("DrawLine not implemented — requires white placeholder texture");
    }

    /// <summary>
    /// Renders a text string using a bitmap font.
    /// Stub — requires BitmapFont system (Task 043).
    /// </summary>
    /// <param name="text">String to render.</param>
    /// <param name="x">World X position in pixels.</param>
    /// <param name="y">World Y position in pixels.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer.</param>
    public void DrawString(string text, float x, float y, uint color, float layer)
    {
        _logger.LogWarning("DrawString not implemented — requires BitmapFont");
    }
}
