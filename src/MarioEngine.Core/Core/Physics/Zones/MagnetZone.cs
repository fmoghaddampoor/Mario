namespace MarioEngine.Core.Core.Physics.Zones;

/// <summary>Attracts physics objects toward a center point.</summary>
internal sealed class MagnetZone
{
    public Vector2 Center { get; set; }
    public float Range { get; set; } = 200f;
    public float Strength { get; set; } = 50f;

    public Vector2 GetPullForce(Vector2 objectPos)
    {
        float dx = Center.X - objectPos.X;
        float dy = Center.Y - objectPos.Y;
        float dist = (float)Math.Sqrt(dx * dx + dy * dy);
        if (dist > Range || dist < 0.1f) return default;
        float magnitude = Strength * (1f - dist / Range);
        return new Vector2(dx / dist * magnitude, dy / dist * magnitude);
    }
}
