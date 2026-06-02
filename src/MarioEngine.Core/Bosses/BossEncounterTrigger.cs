using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossEncounterTrigger
{
    public Vector2 TriggerPosition { get; set; }
    public float TriggerRadius { get; set; }
    public bool IsTriggered { get; set; }
    public bool IsBossDefeated { get; set; }

    public void CheckTrigger(Vector2 playerPos)
    {
        if (IsTriggered || IsBossDefeated) return;

        float dist = Vector2.Distance(playerPos, TriggerPosition);
        if (dist <= TriggerRadius)
        {
            OnEncounterStart();
        }
    }

    public void OnEncounterStart()
    {
        IsTriggered = true;
    }
}
