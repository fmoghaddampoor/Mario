namespace MarioEngine.Core.UI;

/// <summary>Shows remaining lives as icons.</summary>
internal sealed class LivesDisplay
{
    /// <summary>Number of lives to display.</summary>
    public int DisplayLives { get; private set; }

    /// <summary>Updates the displayed lives count.</summary>
    public void Update(int lives)
    {
        DisplayLives = lives;
    }
}
