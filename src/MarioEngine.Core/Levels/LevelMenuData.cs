namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Data for rendering the world map menu.</summary>
internal sealed class LevelMenuData
{
    /// <summary>Name of the world.</summary>
    public string WorldName { get; set; } = string.Empty;

    /// <summary>Level names in this world.</summary>
    public List<string> LevelNames { get; set; } = new();

    /// <summary>Path to the preview image.</summary>
    public string PreviewImage { get; set; } = string.Empty;
}
