namespace MarioEngine.Core.Graphics;

using System;
using System.Numerics;

/// <summary>
/// 2D camera that converts world/pixel coordinates to clip space (-1 to 1).
/// Supports position, zoom, rotation, and screen shake.
/// </summary>
public sealed class Camera2D
{
    private Vector2 _position;
    private float _zoom = 1f;
    private float _rotation;
    private Vector2 _shakeOffset;
    private float _shakeIntensity;
    private float _shakeDuration;
    private float _shakeElapsed;
    private float _viewportWidth = 1920f;
    private float _viewportHeight = 1080f;

    /// <summary>Gets or sets the camera position in world pixels (center of the screen).</summary>
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    /// <summary>Gets or sets the camera zoom level. 1.0 = normal.</summary>
    public float Zoom
    {
        get => _zoom;
        set => _zoom = Math.Max(0.01f, value);
    }

    /// <summary>Gets or sets the camera rotation in radians.</summary>
    public float Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }

    /// <summary>Gets or sets the viewport width in pixels.</summary>
    public float ViewportWidth
    {
        get => _viewportWidth;
        set => _viewportWidth = value;
    }

    /// <summary>Gets or sets the viewport height in pixels.</summary>
    public float ViewportHeight
    {
        get => _viewportHeight;
        set => _viewportHeight = value;
    }

    /// <summary>Gets the current screen shake offset in clip space.</summary>
    public Vector2 ShakeOffset => _shakeOffset;

    /// <summary>
    /// Converts a world pixel position to clip space (-1 to 1).
    /// </summary>
    /// <param name="worldX">World X in pixels.</param>
    /// <param name="worldY">World Y in pixels.</param>
    /// <returns>Clip space coordinates.</returns>
    public Vector2 WorldToClip(float worldX, float worldY)
    {
        var halfW = _viewportWidth * 0.5f / _zoom;
        var halfH = _viewportHeight * 0.5f / _zoom;
        var cx = ((worldX - _position.X) / halfW) + _shakeOffset.X;
        var cy = -((worldY - _position.Y) / halfH) + _shakeOffset.Y;
        return new Vector2(cx, cy);
    }

    /// <summary>
    /// Converts world pixel dimensions to clip space dimensions.
    /// </summary>
    /// <param name="worldWidth">Width in world pixels.</param>
    /// <param name="worldHeight">Height in world pixels.</param>
    /// <returns>Clip space dimensions.</returns>
    public Vector2 WorldSizeToClip(float worldWidth, float worldHeight)
    {
        var halfW = _viewportWidth * 0.5f / _zoom;
        var halfH = _viewportHeight * 0.5f / _zoom;
        return new Vector2(worldWidth / halfW, worldHeight / halfH);
    }

    /// <summary>
    /// Triggers a screen shake effect.
    /// </summary>
    /// <param name="intensity">Maximum shake offset in pixels.</param>
    /// <param name="duration">Duration in seconds.</param>
    public void Shake(float intensity, float duration)
    {
        _shakeIntensity = intensity;
        _shakeDuration = duration;
        _shakeElapsed = 0f;
    }

    /// <summary>
    /// Updates the shake animation. Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds since the last frame.</param>
#pragma warning disable CA5394 // Random is for visual effects, not security
    public void Update(float dt)
    {
        if (_shakeElapsed < _shakeDuration)
        {
            _shakeElapsed += dt;
            var progress = _shakeElapsed / _shakeDuration;
            var fade = 1f - progress;
            var rand = new Random((int)(_shakeElapsed * 1000f));
            var sx = (float)(((rand.NextDouble() * 2.0) - 1.0) * _shakeIntensity * fade);
            var sy = (float)(((rand.NextDouble() * 2.0) - 1.0) * _shakeIntensity * fade);
            var halfW = _viewportWidth * 0.5f;
            var halfH = _viewportHeight * 0.5f;
            _shakeOffset = new Vector2(sx / halfW, sy / halfH);

            if (_shakeElapsed >= _shakeDuration)
            {
                _shakeOffset = Vector2.Zero;
            }
        }
    }
#pragma warning restore CA5394
}
