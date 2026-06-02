using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossMusicController
{
    public string NormalTheme { get; set; } = string.Empty;
    public string PhaseTheme { get; set; } = string.Empty;
    public string DefeatStinger { get; set; } = string.Empty;
    public bool IsPhaseMusicPlaying { get; set; }

    public void TransitionToPhase()
    {
        IsPhaseMusicPlaying = true;
    }

    public void TransitionToDefeat()
    {
        IsPhaseMusicPlaying = false;
    }
}
