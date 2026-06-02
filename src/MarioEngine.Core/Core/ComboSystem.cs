namespace MarioEngine.Core.Core;

/// <summary>Tracks consecutive hits for score multiplier bonuses.</summary>
internal sealed class ComboSystem
{
    public int ComboCount { get; private set; }
    public float ComboTimer { get; private set; }
    public float ComboTimeout { get; set; } = 1.5f;

    public void RegisterHit()
    {
        ComboCount++;
        ComboTimer = 0f;
    }

    public void Update(float dt)
    {
        if (ComboCount == 0) return;
        ComboTimer += dt;
        if (ComboTimer >= ComboTimeout)
        {
            ComboCount = 0;
            ComboTimer = 0f;
        }
    }

    public int GetScoreMultiplier() => ComboCount switch
    {
        0 => 1,
        <=5 => 2,
        <=10 => 3,
        <=20 => 5,
        _ => 10
    };
}
