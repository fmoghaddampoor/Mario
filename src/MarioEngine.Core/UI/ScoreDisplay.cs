namespace MarioEngine.Core.UI;

/// <summary>Animated score counter with roll-up effect and combo multiplier display.</summary>
internal sealed class ScoreDisplay
{
    /// <summary>Currently displayed score.</summary>
    public int DisplayScore { get; private set; }

    /// <summary>Target score to animate toward.</summary>
    public int TargetScore { get; private set; }

    /// <summary>Sets the target score.</summary>
    public void SetTarget(int score)
    {
        TargetScore = score;
    }

    /// <summary>Updates the animation each frame.</summary>
    public void Update(float dt)
    {
    }
}
