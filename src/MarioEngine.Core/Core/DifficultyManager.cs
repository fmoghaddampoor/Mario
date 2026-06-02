namespace MarioEngine.Core.Core;

/// <summary>Scales game difficulty parameters.</summary>
internal enum Difficulty { Easy, Normal, Hard, Insane }

internal sealed class DifficultyManager
{
    public Difficulty CurrentDifficulty { get; set; } = Difficulty.Normal;

    public float GetDamageMultiplier() => CurrentDifficulty switch
    {
        Difficulty.Easy => 0.5f,
        Difficulty.Normal => 1.0f,
        Difficulty.Hard => 1.5f,
        Difficulty.Insane => 2.0f,
        _ => 1.0f
    };

    public float GetEnemyHealthMultiplier() => CurrentDifficulty switch
    {
        Difficulty.Easy => 0.7f,
        Difficulty.Normal => 1.0f,
        Difficulty.Hard => 1.8f,
        Difficulty.Insane => 3.0f,
        _ => 1.0f
    };

    public float GetCoinMultiplier() => CurrentDifficulty switch
    {
        Difficulty.Easy => 2.0f,
        Difficulty.Normal => 1.0f,
        Difficulty.Hard => 0.5f,
        Difficulty.Insane => 0.25f,
        _ => 1.0f
    };
}
