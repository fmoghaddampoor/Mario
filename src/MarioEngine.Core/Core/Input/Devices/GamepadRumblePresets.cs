namespace MarioEngine.Core.Core.Input.Devices;

/// <summary>Predefined rumble patterns for gamepad feedback.</summary>
internal static class GamepadRumblePresets
{
    private static readonly Dictionary<string, (float Left, float Right)> Presets = new()
    {
        { "Light", (0.2f, 0.2f) },
        { "Medium", (0.5f, 0.5f) },
        { "Heavy", (1.0f, 1.0f) },
        { "StaggerLeft", (1.0f, 0.0f) },
        { "StaggerRight", (0.0f, 1.0f) },
    };

    public static (float Left, float Right) GetPreset(string name)
    {
        return Presets.TryGetValue(name, out var p) ? p : (0f, 0f);
    }
}
