namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Manages worlds and progression between them.</summary>
internal sealed class WorldManager
{
    /// <summary>Current world index.</summary>
    public int CurrentWorld { get; set; }

    /// <summary>All worlds.</summary>
    public List<WorldMap> Worlds { get; } = new();

    /// <summary>Unlocks the next world when the current one is completed.</summary>
    public void UnlockNextWorld()
    {
        if (CurrentWorld + 1 < Worlds.Count)
            CurrentWorld++;
    }

    /// <summary>Gets the current world map, or null.</summary>
    public WorldMap? GetCurrentWorld()
    {
        return CurrentWorld >= 0 && CurrentWorld < Worlds.Count ? Worlds[CurrentWorld] : null;
    }
}
