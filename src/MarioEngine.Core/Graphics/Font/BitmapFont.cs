namespace MarioEngine.Core.Graphics.Font;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Logging;

/// <summary>
/// Loads a bitmap font from a BMFont (.fnt) format file and its associated texture atlas.
/// Supports ASCII characters 32-127, kerning, text wrapping, alignment, color tint, and drop shadow.
/// </summary>
public sealed class BitmapFont
{
    /// <summary>Logger for font loading events.</summary>
    private readonly ILogger<BitmapFont> _logger;

    /// <summary>Character data keyed by ASCII code (32-127).</summary>
    private readonly Dictionary<int, FontCharacter> _characters;

    /// <summary>Kerning pairs mapping (first, second) to pixel offset.</summary>
    private readonly Dictionary<(int First, int Second), int> _kernings;

    /// <summary>OpenGL handle of the font atlas texture.</summary>
    private int _atlasTextureHandle;

    /// <summary>Atlas texture width in pixels, used for UV calculation.</summary>
    private float _atlasWidth;

    /// <summary>Atlas texture height in pixels, used for UV calculation.</summary>
    private float _atlasHeight;

    /// <summary>Initializes a new instance of the <see cref="BitmapFont"/> class.</summary>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if logger is null.</exception>
    public BitmapFont(ILogger<BitmapFont> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _characters = new Dictionary<int, FontCharacter>();
        _kernings = new Dictionary<(int, int), int>();
    }

    /// <summary>Gets the baseline font height in pixels.</summary>
    public int LineHeight { get; private set; }

    /// <summary>Gets the atlas texture handle for rendering.</summary>
    public int AtlasTextureHandle => _atlasTextureHandle;

    /// <summary>
    /// Loads a BMFont (.fnt) file from disk and associates it with a texture atlas.
    /// </summary>
    /// <param name="fntPath">Path to the .fnt file.</param>
    /// <param name="atlasTextureHandle">OpenGL handle of the loaded font atlas texture.</param>
    /// <param name="atlasWidth">Atlas texture width in pixels.</param>
    /// <param name="atlasHeight">Atlas texture height in pixels.</param>
    public void LoadFnt(string fntPath, uint atlasTextureHandle, int atlasWidth, int atlasHeight)
    {
        _atlasTextureHandle = (int)atlasTextureHandle;
        _atlasWidth = atlasWidth;
        _atlasHeight = atlasHeight;
        _characters.Clear();
        _kernings.Clear();

        var lines = File.ReadAllLines(fntPath);
        foreach (var line in lines)
        {
            if (line.StartsWith("common ", StringComparison.Ordinal))
            {
                ParseCommon(line);
            }
            else if (line.StartsWith("char ", StringComparison.Ordinal))
            {
                ParseChar(line);
            }
            else if (line.StartsWith("kerning ", StringComparison.Ordinal))
            {
                ParseKerning(line);
            }
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(
                "Loaded font: {Chars} chars, {Kerns} kernings from {Path}",
                _characters.Count,
                _kernings.Count,
                fntPath);
        }
    }

    /// <summary>
    /// Gets the character data for a given ASCII character.
    /// </summary>
    /// <param name="c">The character to look up.</param>
    /// <returns>FontCharacter data, or null if not found.</returns>
    public FontCharacter? GetCharacter(char c)
    {
        _characters.TryGetValue(c, out var ch);
        return ch;
    }

    /// <summary>
    /// Gets the kerning offset between two consecutive characters.
    /// </summary>
    /// <param name="first">Previous character.</param>
    /// <param name="second">Current character.</param>
    /// <returns>Kerning offset in pixels.</returns>
    public int GetKerning(char first, char second)
    {
        _kernings.TryGetValue((first, second), out var amount);
        return amount;
    }

    /// <summary>
    /// Measures the pixel width of a text string.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <returns>Width in pixels.</returns>
    public float MeasureText(string text)
    {
        return TextLayout.MeasureText(text, this);
    }

    private static Dictionary<string, string> ParseKeyValues(string line)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (var i = 1; i < parts.Length; i++)
        {
            var eq = parts[i].IndexOf('=');
            if (eq > 0)
            {
                var key = parts[i].Substring(0, eq);
                var value = parts[i].Substring(eq + 1);
                result[key] = value;
            }
        }

        return result;
    }

    private static int GetInt(Dictionary<string, string> parts, string key)
    {
        return parts.TryGetValue(key, out var val) && int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : 0;
    }

    private void ParseCommon(string line)
    {
        var parts = ParseKeyValues(line);
        if (parts.TryGetValue("lineHeight", out var lh))
        {
            LineHeight = int.Parse(lh, CultureInfo.InvariantCulture);
        }
    }

    private void ParseChar(string line)
    {
        var parts = ParseKeyValues(line);
        if (!parts.TryGetValue("id", out var idStr) || !int.TryParse(idStr, out var id))
        {
            return;
        }

        var x = GetInt(parts, "x");
        var y = GetInt(parts, "y");
        var w = GetInt(parts, "width");
        var h = GetInt(parts, "height");
        var ox = GetInt(parts, "xoffset");
        var oy = GetInt(parts, "yoffset");
        var ax = GetInt(parts, "xadvance");

        _characters[id] = new FontCharacter(id, x, y, w, h, ox, oy, ax, (int)_atlasWidth, (int)_atlasHeight);
    }

    private void ParseKerning(string line)
    {
        var parts = ParseKeyValues(line);
        var first = GetInt(parts, "first");
        var second = GetInt(parts, "second");
        var amount = GetInt(parts, "amount");
        if (amount != 0)
        {
            _kernings[(first, second)] = amount;
        }
    }
}
