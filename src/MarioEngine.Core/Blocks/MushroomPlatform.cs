using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class MushroomPlatform : BlockBase
{
    public List<Vector2> Waypoints { get; set; } = new();
    public float Speed { get; set; } = 60f;
    public int CurrentWaypoint { get; set; }

    public MushroomPlatform()
    {
        IsSolid = true;
        IsBreakable = false;
    }

    public void Update(float dt)
    {
        if (Waypoints.Count == 0) return;

        Vector2 target = Waypoints[CurrentWaypoint];
        Vector2 direction = Vector2.Normalize(target - Position);
        Position += direction * Speed * dt;

        if (Vector2.Distance(Position, target) < 1f)
        {
            CurrentWaypoint = (CurrentWaypoint + 1) % Waypoints.Count;
        }
    }
}
