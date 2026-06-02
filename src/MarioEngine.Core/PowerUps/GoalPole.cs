using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class GoalPole
{
    public Vector2 Position { get; set; }
    public float Height { get; set; }
    public bool Reached { get; private set; }

    public void OnReach(Player player)
    {
        if (Reached) return;
        Reached = true;
    }
}
