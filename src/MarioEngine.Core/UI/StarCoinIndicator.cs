namespace MarioEngine.Core.UI;

/// <summary>Shows 3 star coin slots per level.</summary>
internal sealed class StarCoinIndicator
{
    /// <summary>Collected state for each star coin slot.</summary>
    public bool[] Collected { get; } = new bool[3];

    /// <summary>Updates the indicator with collection states.</summary>
    public void Update(bool c1, bool c2, bool c3)
    {
        Collected[0] = c1;
        Collected[1] = c2;
        Collected[2] = c3;
    }
}
