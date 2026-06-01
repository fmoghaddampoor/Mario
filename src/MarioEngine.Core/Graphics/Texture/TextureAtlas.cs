namespace MarioEngine.Core.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// Represents a texture atlas — a single large texture containing multiple sub-textures (regions).
/// Loaded from a JSON metadata file that maps region names to their rectangles in the atlas.
/// Compatible with TexturePacker JSON (Array) format.
/// </summary>
public sealed class TextureAtlas
{
    /// <summary>Region lookup by name, case-insensitive.</summary>
    private readonly Dictionary<string, AtlasRegion> _regions;

    private TextureAtlas(Dictionary<string, AtlasRegion> regions)
    {
        _regions = regions;
    }

    /// <summary>Gets the underlying atlas texture.</summary>
    public Texture2D Texture { get; private set; } = null!;

    /// <summary>Gets the number of regions in this atlas.</summary>
    public int RegionCount => _regions.Count;

    /// <summary>
    /// Loads an atlas from a JSON metadata file.
    /// </summary>
    /// <param name="texture">The atlas texture. Must not be null.</param>
    /// <param name="jsonPath">Path to the JSON metadata file.</param>
    /// <returns>A populated TextureAtlas.</returns>
    /// <exception cref="ArgumentNullException">Thrown if texture is null.</exception>
    public static TextureAtlas FromJson(Texture2D texture, string jsonPath)
    {
        ArgumentNullException.ThrowIfNull(texture);
        var json = File.ReadAllText(jsonPath);
        var doc = JsonDocument.Parse(json);
        var regions = new Dictionary<string, AtlasRegion>(StringComparer.OrdinalIgnoreCase);

        if (doc.RootElement.TryGetProperty("frames", out var frames))
        {
            foreach (var entry in frames.EnumerateObject())
            {
                var frame = entry.Value.GetProperty("frame");
                var x = frame.GetProperty("x").GetInt32();
                var y = frame.GetProperty("y").GetInt32();
                var w = frame.GetProperty("w").GetInt32();
                var h = frame.GetProperty("h").GetInt32();

                regions[entry.Name] = new AtlasRegion
                {
                    Name = entry.Name,
                    X = x,
                    Y = y,
                    Width = w,
                    Height = h,
                    U1 = (float)x / texture.Width,
                    V1 = (float)y / texture.Height,
                    U2 = (float)(x + w) / texture.Width,
                    V2 = (float)(y + h) / texture.Height,
                };
            }
        }

        return new TextureAtlas(regions) { Texture = texture };
    }

    /// <summary>
    /// Gets a region by name.
    /// </summary>
    /// <param name="name">Region name (e.g. "mario_run_01").</param>
    /// <returns>The atlas region, or null if not found.</returns>
    public AtlasRegion? GetRegion(string name)
    {
        return _regions.TryGetValue(name, out var region) ? region : null;
    }
}
