namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Represents a world map with level nodes and connections.</summary>
internal sealed class WorldMap
{
    /// <summary>World name.</summary>
    public string WorldName { get; set; } = string.Empty;

    /// <summary>Level nodes on the map.</summary>
    public List<LevelNode> Nodes { get; set; } = new();

    /// <summary>Node connections as index pairs.</summary>
    public List<(int, int)> Connections { get; set; } = new();

    /// <summary>Renders the world map.</summary>
    public void Render()
    {
    }
}
