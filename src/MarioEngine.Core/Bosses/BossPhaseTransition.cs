using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossPhaseTransition
{
    public float TransitionDuration { get; set; }
    public float Timer { get; set; }
    public bool IsTransitioning { get; private set; }

    public void Start()
    {
        IsTransitioning = true;
        Timer = 0f;
    }

    public void Update(float dt)
    {
        if (!IsTransitioning) return;

        Timer += dt;

        if (Timer >= TransitionDuration)
        {
            IsTransitioning = false;
            Timer = TransitionDuration;
        }
    }
}
