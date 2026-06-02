namespace MarioEngine.Core.Core.Physics;

/// <summary>Simple verlet rope simulation.</summary>
internal sealed class RopePhysics
{
    public List<Vector2> Segments { get; } = new();
    public float SegmentLength { get; set; } = 10f;

    private readonly List<Vector2> _prevPositions = new();

    public void Simulate(float dt)
    {
        if (Segments.Count < 2) return;

        while (_prevPositions.Count < Segments.Count)
            _prevPositions.Add(Segments[_prevPositions.Count]);

        for (int i = 1; i < Segments.Count; i++)
        {
            var temp = Segments[i];
            Segments[i] += Segments[i] - _prevPositions[i];
            _prevPositions[i] = temp;

            var diff = Segments[i] - Segments[i - 1];
            float dist = (float)Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
            if (dist > SegmentLength)
            {
                float correction = (dist - SegmentLength) / dist;
                Segments[i] -= diff * correction;
            }
        }
    }
}
