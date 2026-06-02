namespace MarioEngine.Core.Physics.Collision;

using System.Numerics;

/// <summary>
/// Static raycast helper for casting rays against AABB targets.
/// </summary>
internal static class Raycast
{
    /// <summary>
    /// Casts a ray against an array of AABB targets. Returns the closest hit, if any.
    /// </summary>
    /// <param name="ray">The ray to cast.</param>
    /// <param name="targets">Array of AABB targets to test against.</param>
    /// <returns>The closest <see cref="HitResult"/>, or a miss result.</returns>
    internal static HitResult Cast(Ray ray, Aabb[] targets)
    {
        var bestDist = float.MaxValue;
        var hit = false;
        var point = Vector2.Zero;
        var normal = Vector2.Zero;

        foreach (var target in targets)
        {
            if (!IntersectRayAabb(ray, target, out var tMin, out var tMax))
            {
                continue;
            }

            if (tMin < 0f)
            {
                tMin = tMax;
            }

            if (tMin >= 0f && tMin < bestDist)
            {
                bestDist = tMin;
                hit = true;
                point = ray.Origin + (ray.Direction * tMin);
                normal = ComputeNormal(ray, target, tMin);
            }
        }

        return new HitResult(hit, bestDist, point, normal);
    }

    private static bool IntersectRayAabb(Ray ray, Aabb aabb, out float tMin, out float tMax)
    {
        tMin = float.MinValue;
        tMax = float.MaxValue;
        var min = aabb.Min;
        var max = aabb.Max;

        for (var axis = 0; axis < 2; axis++)
        {
            var invD = 1f / (axis == 0 ? ray.Direction.X : ray.Direction.Y);
            var t0 = ((axis == 0 ? min.X : min.Y) - (axis == 0 ? ray.Origin.X : ray.Origin.Y)) * invD;
            var t1 = ((axis == 0 ? max.X : max.Y) - (axis == 0 ? ray.Origin.X : ray.Origin.Y)) * invD;

            if (invD < 0f)
            {
                (t0, t1) = (t1, t0);
            }

            tMin = MathF.Max(tMin, t0);
            tMax = MathF.Min(tMax, t1);

            if (tMax < tMin)
            {
                return false;
            }
        }

        return true;
    }

    private static Vector2 ComputeNormal(Ray ray, Aabb aabb, float t)
    {
        var hitPoint = ray.Origin + (ray.Direction * t);
        var center = aabb.Center;
        var half = aabb.HalfExtents;
        var dx = (hitPoint.X - center.X) / half.X;
        var dy = (hitPoint.Y - center.Y) / half.Y;

        if (MathF.Abs(dx) > MathF.Abs(dy))
        {
            return new Vector2(MathF.Sign(dx), 0f);
        }

        return new Vector2(0f, MathF.Sign(dy));
    }

    /// <summary>
    /// A ray defined by an origin point and a direction vector.
    /// </summary>
    internal readonly struct Ray
    {
        /// <summary>Starting point of the ray.</summary>
        internal readonly Vector2 Origin;

        /// <summary>Direction of the ray (should be normalized).</summary>
        internal readonly Vector2 Direction;

        /// <summary>Initializes a new instance of the <see cref="Ray"/> struct.</summary>
        /// <param name="origin">Starting point.</param>
        /// <param name="direction">Direction vector.</param>
        internal Ray(Vector2 origin, Vector2 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }

    /// <summary>
    /// Result of a raycast hit against a target.
    /// </summary>
    internal readonly struct HitResult
    {
        /// <summary>Whether the ray hit a target.</summary>
        internal readonly bool Hit;

        /// <summary>Distance along the ray to the hit point.</summary>
        internal readonly float Distance;

        /// <summary>World position of the hit point.</summary>
        internal readonly Vector2 Point;

        /// <summary>Surface normal at the hit point.</summary>
        internal readonly Vector2 Normal;

        /// <summary>Initializes a new instance of the <see cref="HitResult"/> struct.</summary>
        /// <param name="hit">Whether a hit occurred.</param>
        /// <param name="distance">Distance to the hit.</param>
        /// <param name="point">World position of the hit.</param>
        /// <param name="normal">Surface normal at the hit.</param>
        internal HitResult(bool hit, float distance, Vector2 point, Vector2 normal)
        {
            Hit = hit;
            Distance = distance;
            Point = point;
            Normal = normal;
        }
    }
}
