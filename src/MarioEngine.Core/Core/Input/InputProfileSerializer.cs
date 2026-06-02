namespace MarioEngine.Core.Core.Input;

/// <summary>Serializes and deserializes input action maps to JSON.</summary>
internal static class InputProfileSerializer
{
    public static void Save(string path, InputActionMap map)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(map);
        File.WriteAllText(path, json);
    }

    public static InputActionMap Load(string path)
    {
        if (!File.Exists(path)) return new InputActionMap();
        var json = File.ReadAllText(path);
        return System.Text.Json.JsonSerializer.Deserialize<InputActionMap>(json) ?? new InputActionMap();
    }
}

internal sealed class InputActionMap
{
    public Dictionary<string, string> Bindings { get; set; } = new();
}
