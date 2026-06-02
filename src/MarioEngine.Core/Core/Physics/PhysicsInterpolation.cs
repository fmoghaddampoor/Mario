namespace MarioEngine.Core.Core.Physics;

/// <summary>Smooth interpolation between fixed physics steps.</summary>
internal static class PhysicsInterpolation
{
    public static Vector2 Interpolate(Vector2 previous, Vector2 current, float alpha)
    {
        return new Vector2(
            previous.X + (current.X - previous.X) * alpha,
            previous.Y + (current.Y - previous.Y) * alpha);
    }
}
