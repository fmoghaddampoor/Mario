namespace MarioEngine.Core.Physics.Collision;

using System;

/// <summary>
/// Delegate invoked when two AABBs overlap.
/// </summary>
internal delegate void CollisionCallback(Aabb a, Aabb b);

/// <summary>
/// Stores collision callbacks for registered body pairs.
/// </summary>
internal sealed class CollisionCallbacks
{
    private event CollisionCallback? OnCollide;

    internal void Subscribe(CollisionCallback callback) => OnCollide += callback;
    internal void Unsubscribe(CollisionCallback callback) => OnCollide -= callback;
    internal void Invoke(Aabb a, Aabb b) => OnCollide?.Invoke(a, b);
}
