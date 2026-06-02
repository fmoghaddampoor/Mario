using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossCameraSystem
{
    public Vector2 FocusPoint { get; set; }
    public float ZoomLevel { get; set; } = 1.5f;
    public float SmoothSpeed { get; set; } = 0.05f;

    public void FocusOnBoss(Vector2 bossPos)
    {
        FocusPoint = bossPos;
    }

    public void FocusOnPlayer(Vector2 playerPos)
    {
        FocusPoint = playerPos;
    }

    public void Reset()
    {
        FocusPoint = Vector2.Zero;
        ZoomLevel = 1f;
    }
}
