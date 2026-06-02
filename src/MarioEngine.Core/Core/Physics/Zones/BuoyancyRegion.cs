namespace MarioEngine.Core.Core.Physics.Zones;

/// <summary>Water zone that applies buoyancy force.</summary>
internal sealed class BuoyancyRegion
{
    public Aabb Region { get; set; }
    public float Density { get; set; } = 1.0f;

    public float GetBuoyancyForce(Vector2 position)
    {
        if (!Region.Contains(position)) return 0f;
        return Density * 9.81f;
    }
}
