namespace MarioEngine.Core.UI;

/// <summary>Animated coin counter with pop-up effect.</summary>
internal sealed class CoinCounter
{
    /// <summary>Currently displayed count.</summary>
    public int DisplayCount { get; private set; }

    /// <summary>Target count to animate toward.</summary>
    public int TargetCount { get; private set; }

    /// <summary>Sets the target coin count.</summary>
    public void SetTarget(int count)
    {
        TargetCount = count;
    }

    /// <summary>Updates the animation each frame.</summary>
    public void Update(float dt)
    {
    }
}
