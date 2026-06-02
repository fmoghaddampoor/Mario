namespace MarioEngine.Core.UI;

/// <summary>Heads-up display rendering health, coins, score, lives, time, power-up.</summary>
internal sealed class HUD
{
    /// <summary>Score display position.</summary>
    public Vector2 ScorePosition { get; set; }

    /// <summary>Coin display position.</summary>
    public Vector2 CoinPosition { get; set; }

    /// <summary>Time display position.</summary>
    public Vector2 TimePosition { get; set; }

    /// <summary>Updates HUD from player state.</summary>
    public void Update(Player player)
    {
    }

    /// <summary>Renders the HUD.</summary>
    public void Render()
    {
    }
}
