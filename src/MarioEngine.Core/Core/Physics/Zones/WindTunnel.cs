namespace MarioEngine.Core.Core.Physics.Zones;

/// <summary>Directional wind zone affecting physics objects.</summary>
internal sealed class WindTunnel
{
    public Aabb Region { get; set; }
    public Vector2 Force { get; set; }
    public float Strength { get; set; } = 1.0f;

    public Vector2 GetForce(Vector2 position)
    {
        if (!Region.Contains(position)) return default;
        return new Vector2(Force.X * Strength, Force.Y * Strength);
    }
}
