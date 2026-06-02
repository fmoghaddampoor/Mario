namespace MarioEngine.Core.Core.Physics.Collision;

/// <summary>Linked physics bodies forming a chain.</summary>
internal sealed class ChainCollision
{
    public List<RigidBody> Links { get; } = new();
    public float LinkDistance { get; set; }

    public void Update()
    {
        for (int i = 1; i < Links.Count; i++)
        {
            var a = Links[i - 1].Position;
            var b = Links[i].Position;
            float dist = (float)Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
            if (dist > LinkDistance)
            {
                float dx = (b.X - a.X) / dist * LinkDistance;
                float dy = (b.Y - a.Y) / dist * LinkDistance;
                Links[i].Position = new Vector2(a.X + dx, a.Y + dy);
            }
        }
    }
}

internal sealed class RigidBody
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
}
