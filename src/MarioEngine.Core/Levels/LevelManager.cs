namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Manages loading, unloading, and updating levels.</summary>
internal sealed class LevelManager
{
    /// <summary>Currently active level, if any.</summary>
    public Level? CurrentLevel { get; private set; }

    /// <summary>All registered levels keyed by name.</summary>
    public Dictionary<string, Level> Levels { get; } = new();

    /// <summary>Loads a level by name, unloading the current one.</summary>
    public void LoadLevel(string name)
    {
        if (CurrentLevel is not null)
            CurrentLevel.Unload();

        if (Levels.TryGetValue(name, out var level))
        {
            level.Load();
            CurrentLevel = level;
        }
    }

    /// <summary>Unloads the current level.</summary>
    public void UnloadLevel()
    {
        CurrentLevel?.Unload();
        CurrentLevel = null;
    }

    /// <summary>Updates the current level each frame.</summary>
    public void Update(float dt)
    {
    }
}
