namespace MarioEngine.Core.Graphics.Animation;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// Loads animation definitions from JSON files.
/// Expected format: root object with "animations" array containing named animation entries.
/// Each entry has "name", "loop", and "frames" properties.
/// Each frame has u1, v1, u2, v2, and duration.
/// </summary>
public static class AnimationDefinition
{
    /// <summary>
    /// Loads animation definitions from a JSON file.
    /// </summary>
    /// <param name="jsonPath">Path to the JSON animation definition file.</param>
    /// <returns>A dictionary of animation clips keyed by name.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    /// <exception cref="JsonException">Thrown if the JSON is malformed.</exception>
    public static Dictionary<string, AnimationClip> LoadFromJson(string jsonPath)
    {
        var json = File.ReadAllText(jsonPath);
        var doc = JsonDocument.Parse(json);
        var result = new Dictionary<string, AnimationClip>(StringComparer.OrdinalIgnoreCase);

        if (!doc.RootElement.TryGetProperty("animations", out var animations))
        {
            return result;
        }

        foreach (var entry in animations.EnumerateArray())
        {
            var name = entry.GetProperty("name").GetString() ?? "unnamed";
            var loop = entry.TryGetProperty("loop", out var loopProp) && loopProp.GetBoolean();
            var frames = new List<AnimationFrame>();

            if (entry.TryGetProperty("frames", out var framesProp))
            {
                foreach (var frameEntry in framesProp.EnumerateArray())
                {
                    var u1 = frameEntry.GetProperty("u1").GetSingle();
                    var v1 = frameEntry.GetProperty("v1").GetSingle();
                    var u2 = frameEntry.GetProperty("u2").GetSingle();
                    var v2 = frameEntry.GetProperty("v2").GetSingle();
                    var duration = frameEntry.GetProperty("duration").GetSingle();
                    frames.Add(new AnimationFrame(u1, v1, u2, v2, duration));
                }
            }

            result[name] = new AnimationClip(name, frames, loop);
        }

        return result;
    }
}
