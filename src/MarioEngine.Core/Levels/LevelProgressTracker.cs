namespace MarioEngine.Core.Levels;

using System.Collections.Generic;
using System.Linq;

/// <summary>Tracks overall progress across all levels.</summary>
internal sealed class LevelProgressTracker
{
    /// <summary>Progress for each level keyed by name.</summary>
    public Dictionary<string, LevelState> Progress { get; } = new();

    /// <summary>Completion percentage (0-100).</summary>
    public float CompletionPercent
    {
        get
        {
            if (Progress.Count == 0)
                return 0f;

            int completed = Progress.Values.Count(s => s.Status == LevelStatus.Completed);
            return (float)completed / Progress.Count * 100f;
        }
    }

    /// <summary>Total star coins available across all levels.</summary>
    public int TotalStarCoins => Progress.Count * 3;

    /// <summary>Total star coins collected.</summary>
    public int CollectedStarCoins => Progress.Values.Sum(s => s.StarCoins);

    /// <summary>Marks a level as completed with result data.</summary>
    public void MarkCompleted(string levelName, LevelCompleteData data)
    {
        if (!Progress.TryGetValue(levelName, out var state))
        {
            state = new LevelState { LevelName = levelName };
            Progress[levelName] = state;
        }

        state.Status = LevelStatus.Completed;
        state.StarCoins = data.StarCoins;

        if (data.Score > state.BestScore)
            state.BestScore = data.Score;

        if (data.TimeRemaining < state.BestTime || state.BestTime == 0)
            state.BestTime = (int)data.TimeRemaining;
    }
}
