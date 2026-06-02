using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class BlockAnimator
{
    public float BounceHeight { get; set; } = 8f;
    public float BounceDuration { get; set; } = 0.2f;
    public bool IsAnimating { get; private set; }

    private float _timer;

    public void StartBounce()
    {
        IsAnimating = true;
        _timer = 0f;
    }

    public Vector2 GetAnimatedPosition(Vector2 basePos, float dt)
    {
        if (!IsAnimating) return basePos;

        _timer += dt;
        float t = _timer / BounceDuration;

        if (t >= 1f)
        {
            IsAnimating = false;
            return basePos;
        }

        float offset = (float)Math.Sin(t * Math.PI) * BounceHeight;
        return new Vector2(basePos.X, basePos.Y - offset);
    }
}
