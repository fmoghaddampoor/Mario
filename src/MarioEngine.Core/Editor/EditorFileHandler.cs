using System.Text.Json;

namespace MarioEngine.Core.Editor;

/// <summary>Handles level file save/load with JSON serialization.</summary>
internal sealed class EditorFileHandler
{
    public void SaveLevel(Level level, string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (dir != null) Directory.CreateDirectory(dir);
        var json = JsonSerializer.Serialize(level, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public Level LoadLevel(string filePath)
    {
        if (!File.Exists(filePath)) return new Level();
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Level>(json) ?? new Level();
    }
}
