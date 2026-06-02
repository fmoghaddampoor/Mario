namespace MarioEngine.Core.Levels;

/// <summary>Tracks cumulative statistics across all levels.</summary>
internal sealed class LevelStatistics
{
    /// <summary>Total deaths.</summary>
    public int TotalDeaths { get; private set; }

    /// <summary>Total coins collected.</summary>
    public int TotalCoinsCollected { get; private set; }

    /// <summary>Total enemies defeated.</summary>
    public int TotalEnemiesDefeated { get; private set; }

    /// <summary>Total play time in seconds.</summary>
    public float TotalPlayTime { get; private set; }

    /// <summary>Records a death.</summary>
    public void AddDeath() => TotalDeaths++;

    /// <summary>Records a coin collected.</summary>
    public void AddCoin() => TotalCoinsCollected++;

    /// <summary>Records an enemy defeated.</summary>
    public void AddEnemyDefeated() => TotalEnemiesDefeated++;

    /// <summary>Updates play time each frame.</summary>
    public void Update(float dt) => TotalPlayTime += dt;
}
