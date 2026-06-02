using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles grappling hook ability.</summary>
internal sealed class PlayerGrapplingHook
{
    /// <summary>Whether the grappling hook is active.</summary>
    public bool IsGrappling { get; private set; }

    /// <summary>Target point the hook is attached to.</summary>
    public Vector2 HookPoint { get; private set; }

    /// <summary>Speed at which the player is pulled toward the hook point.</summary>
    public float HookSpeed { get; set; } = 800f;

    /// <summary>Maximum distance the hook can reach.</summary>
    public float MaxHookDistance { get; set; } = 300f;

    /// <summary>Fires the hook from the given position toward the target.</summary>
    public void FireHook(Vector2 from, Vector2 target)
    {
        if (!CanReach(from, target)) return;

        IsGrappling = true;
        HookPoint = target;
    }

    /// <summary>Releases the grappling hook.</summary>
    public void Release()
    {
        IsGrappling = false;
    }

    /// <summary>Pulls the player toward the hook point each frame.</summary>
    public Vector2 UpdateGrapple(Vector2 currentPos, float dt)
    {
        if (!IsGrappling) return currentPos;

        var direction = Vector2.Normalize(HookPoint - currentPos);
        var distance = Vector2.Distance(currentPos, HookPoint);
        var step = Math.Min(HookSpeed * dt, distance);
        return currentPos + direction * step;
    }

    /// <summary>Checks whether the target is within hook range.</summary>
    public bool CanReach(Vector2 from, Vector2 target)
    {
        return Vector2.Distance(from, target) <= MaxHookDistance;
    }
}
