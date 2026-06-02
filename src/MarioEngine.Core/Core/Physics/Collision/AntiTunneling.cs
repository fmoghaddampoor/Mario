namespace MarioEngine.Core.Core.Physics.Collision;

/// <summary>Continuous collision detection to prevent tunneling.</summary>
internal static class AntiTunneling
{
    public static bool WillTunnel(Aabb aabb, Vector2 velocity, float dt)
    {
        float displacementX = velocity.X * dt;
        float displacementY = velocity.Y * dt;
        float halfWidth = (aabb.Max.X - aabb.Min.X) / 2f;
        float halfHeight = (aabb.Max.Y - aabb.Min.Y) / 2f;
        return Math.Abs(displacementX) > halfWidth || Math.Abs(displacementY) > halfHeight;
    }
}
