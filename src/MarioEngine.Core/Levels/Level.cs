namespace MarioEngine.Core.Levels;

using System.Collections.Generic;
using MarioEngine.Core;
using MarioEngine.Core.Levels;

/// <summary>Represents a single playable level.</summary>
internal class Level
{
    /// <summary>Name of the level.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>World name this level belongs to.</summary>
    public string WorldName { get; set; } = string.Empty;

    /// <summary>World number.</summary>
    public int WorldNumber { get; set; }

    /// <summary>Level number within the world.</summary>
    public int LevelNumber { get; set; }

    /// <summary>Player spawn position.</summary>
    public Vector2 PlayerStart { get; set; }

    /// <summary>Optional goal position.</summary>
    public Vector2? GoalPosition { get; set; }

    /// <summary>Entities in the level.</summary>
    public List<Entity> Entities { get; set; } = new();

    /// <summary>Loads the level resources.</summary>
    public void Load()
    {
    }

    /// <summary>Unloads the level resources.</summary>
    public void Unload()
    {
    }
}
