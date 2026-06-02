namespace MarioEngine.Core.Levels;

/// <summary>Manages checkpoints within a level.</summary>
internal sealed class CheckpointManager
{
    /// <summary>Last checkpoint position, or null.</summary>
    public Vector2? LastCheckpoint { get; private set; }

    /// <summary>Whether a checkpoint has been set.</summary>
    public bool HasCheckpoint => LastCheckpoint.HasValue;

    /// <summary>Sets a new checkpoint position.</summary>
    public void SetCheckpoint(Vector2 pos)
    {
        LastCheckpoint = pos;
    }

    /// <summary>Gets the respawn position or null.</summary>
    public Vector2? GetRespawnPosition()
    {
        return LastCheckpoint;
    }
}
