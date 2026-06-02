namespace MarioEngine.Core.Player;

using System;
using System.Numerics;

/// <summary>Provides smooth camera follow behaviour with configurable offset and dead-zone bounds.</summary>
internal sealed class PlayerCameraFollower
{
    private float _minX = float.MinValue;
    private float _maxX = float.MaxValue;
    private float _minY = float.MinValue;
    private float _maxY = float.MaxValue;

    /// <summary>Gets or sets the camera offset relative to the player.</summary>
    public Vector2 Offset { get; set; } = new Vector2(0f, -100f);

    /// <summary>Gets or sets the smoothing factor for camera movement (0–1).</summary>
    public float SmoothSpeed { get; set; } = 0.1f;

    /// <summary>Calculates the target camera position using smooth follow within defined bounds.</summary>
    /// <param name="playerPos">The player's current world position.</param>
    /// <param name="cameraPos">The camera's current position.</param>
    /// <returns>The smoothed, bounded camera position.</returns>
    public Vector2 GetTargetPosition(Vector2 playerPos, Vector2 cameraPos)
    {
        Vector2 desired = playerPos + Offset;
        Vector2 smoothed = Vector2.Lerp(cameraPos, desired, SmoothSpeed);

        float clampedX = Math.Clamp(smoothed.X, _minX, _maxX);
        float clampedY = Math.Clamp(smoothed.Y, _minY, _maxY);

        return new Vector2(clampedX, clampedY);
    }

    /// <summary>Constrains the camera within the specified world-space rectangle.</summary>
    /// <param name="minX">Minimum X boundary.</param>
    /// <param name="maxX">Maximum X boundary.</param>
    /// <param name="minY">Minimum Y boundary.</param>
    /// <param name="maxY">Maximum Y boundary.</param>
    public void SetBounds(float minX, float maxX, float minY, float maxY)
    {
        _minX = minX;
        _maxX = maxX;
        _minY = minY;
        _maxY = maxY;
    }
}
