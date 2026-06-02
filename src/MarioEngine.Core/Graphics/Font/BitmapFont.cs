namespace MarioEngine.Core.Graphics.Font;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// Loads a bitmap font from a BMFont (.fnt) format file and its associated texture atlas.
/// Supports ASCII characters 32-127, kerning, text wrapping, alignment, color tint, and drop shadow.
/// </summary>
public sealed class BitmapFont
{
    private static readonly byte[] FontBitmaps = new byte[]
    {
        // Space (32)
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        // !
        0x08, 0x08, 0x08, 0x08, 0x08, 0x00, 0x08, 0x00,
        // "
        0x14, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00,
        // #
        0x14, 0x14, 0x7F, 0x14, 0x7F, 0x14, 0x14, 0x00,
        // $
        0x08, 0x3E, 0x09, 0x3E, 0x20, 0x3E, 0x08, 0x00,
        // %
        0x63, 0x64, 0x08, 0x10, 0x20, 0x26, 0x63, 0x00,
        // &
        0x3C, 0x24, 0x38, 0x6C, 0x44, 0x48, 0x37, 0x00,
        // '
        0x08, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        // (
        0x0C, 0x10, 0x20, 0x20, 0x20, 0x10, 0x0C, 0x00,
        // )
        0x30, 0x08, 0x04, 0x04, 0x04, 0x08, 0x30, 0x00,
        // *
        0x00, 0x14, 0x08, 0x3E, 0x08, 0x14, 0x00, 0x00,
        // +
        0x00, 0x08, 0x08, 0x3E, 0x08, 0x08, 0x00, 0x00,
        // ,
        0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x08, 0x04,
        // -
        0x00, 0x00, 0x00, 0x3E, 0x00, 0x00, 0x00, 0x00,
        // .
        0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x08, 0x00,
        // /
        0x00, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x00,
        // 0
        0x3E, 0x45, 0x49, 0x51, 0x61, 0x41, 0x3E, 0x00,
        // 1
        0x08, 0x18, 0x28, 0x08, 0x08, 0x08, 0x3E, 0x00,
        // 2
        0x3E, 0x41, 0x02, 0x0C, 0x30, 0x40, 0x7F, 0x00,
        // 3
        0x3E, 0x41, 0x02, 0x1E, 0x02, 0x41, 0x3E, 0x00,
        // 4
        0x04, 0x0C, 0x14, 0x24, 0x7F, 0x04, 0x04, 0x00,
        // 5
        0x7F, 0x40, 0x7E, 0x01, 0x01, 0x41, 0x3E, 0x00,
        // 6
        0x3E, 0x40, 0x7E, 0x41, 0x41, 0x41, 0x3E, 0x00,
        // 7
        0x7F, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x00,
        // 8
        0x3E, 0x41, 0x41, 0x3E, 0x41, 0x41, 0x3E, 0x00,
        // 9
        0x3E, 0x41, 0x41, 0x3F, 0x01, 0x01, 0x3E, 0x00,
        // :
        0x00, 0x08, 0x08, 0x00, 0x00, 0x08, 0x08, 0x00,
        // ;
        0x00, 0x08, 0x08, 0x00, 0x00, 0x08, 0x08, 0x04,
        // <
        0x04, 0x08, 0x10, 0x20, 0x10, 0x08, 0x04, 0x00,
        // =
        0x00, 0x00, 0x3E, 0x00, 0x3E, 0x00, 0x00, 0x00,
        // >
        0x10, 0x08, 0x04, 0x02, 0x04, 0x08, 0x10, 0x00,
        // ?
        0x3E, 0x41, 0x02, 0x0C, 0x08, 0x00, 0x08, 0x00,
        // @
        0x3E, 0x41, 0x5D, 0x55, 0x5D, 0x40, 0x3E, 0x00,
        // A
        0x1C, 0x22, 0x42, 0x7E, 0x42, 0x42, 0x42, 0x00,
        // B
        0x7C, 0x22, 0x22, 0x3C, 0x22, 0x22, 0x7C, 0x00,
        // C
        0x3C, 0x42, 0x40, 0x40, 0x40, 0x42, 0x3C, 0x00,
        // D
        0x78, 0x24, 0x22, 0x22, 0x22, 0x24, 0x78, 0x00,
        // E
        0x7E, 0x40, 0x40, 0x78, 0x40, 0x40, 0x7E, 0x00,
        // F
        0x7E, 0x40, 0x40, 0x78, 0x40, 0x40, 0x40, 0x00,
        // G
        0x3C, 0x42, 0x40, 0x4E, 0x42, 0x42, 0x3C, 0x00,
        // H
        0x42, 0x42, 0x42, 0x7E, 0x42, 0x42, 0x42, 0x00,
        // I
        0x3E, 0x08, 0x08, 0x08, 0x08, 0x08, 0x3E, 0x00,
        // J
        0x1F, 0x02, 0x02, 0x02, 0x02, 0x42, 0x3C, 0x00,
        // K
        0x42, 0x44, 0x48, 0x70, 0x48, 0x44, 0x42, 0x00,
        // L
        0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x7E, 0x00,
        // M
        0x41, 0x63, 0x55, 0x49, 0x41, 0x41, 0x41, 0x00,
        // N
        0x42, 0x62, 0x52, 0x4A, 0x46, 0x42, 0x42, 0x00,
        // O
        0x3C, 0x42, 0x42, 0x42, 0x42, 0x42, 0x3C, 0x00,
        // P
        0x7C, 0x42, 0x42, 0x7C, 0x40, 0x40, 0x40, 0x00,
        // Q
        0x3C, 0x42, 0x42, 0x42, 0x4A, 0x44, 0x3A, 0x00,
        // R
        0x7C, 0x42, 0x42, 0x7C, 0x48, 0x44, 0x42, 0x00,
        // S
        0x3E, 0x40, 0x3C, 0x02, 0x02, 0x41, 0x3E, 0x00,
        // T
        0x7F, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x00,
        // U
        0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x3C, 0x00,
        // V
        0x42, 0x42, 0x42, 0x42, 0x24, 0x24, 0x18, 0x00,
        // W
        0x41, 0x41, 0x41, 0x49, 0x55, 0x63, 0x41, 0x00,
        // X
        0x42, 0x42, 0x24, 0x18, 0x24, 0x42, 0x42, 0x00,
        // Y
        0x42, 0x42, 0x24, 0x18, 0x08, 0x08, 0x08, 0x00,
        // Z
        0x7F, 0x02, 0x04, 0x08, 0x10, 0x20, 0x7F, 0x00,
        // [
        0x1C, 0x10, 0x10, 0x10, 0x10, 0x10, 0x1C, 0x00,
        // \
        0x00, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x00,
        // ]
        0x38, 0x08, 0x08, 0x08, 0x08, 0x08, 0x38, 0x00,
        // ^
        0x08, 0x14, 0x22, 0x00, 0x00, 0x00, 0x00, 0x00,
        // _
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7F, 0x00,
        // `
        0x10, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        // a
        0x00, 0x00, 0x3C, 0x02, 0x3E, 0x46, 0x3B, 0x00,
        // b
        0x40, 0x40, 0x7C, 0x42, 0x42, 0x42, 0x7C, 0x00,
        // c
        0x00, 0x00, 0x3C, 0x40, 0x40, 0x42, 0x3C, 0x00,
        // d
        0x02, 0x02, 0x3E, 0x42, 0x42, 0x42, 0x3E, 0x00,
        // e
        0x00, 0x00, 0x3C, 0x42, 0x7E, 0x40, 0x3C, 0x00,
        // f
        0x1C, 0x22, 0x20, 0x78, 0x20, 0x20, 0x20, 0x00,
        // g
        0x00, 0x00, 0x3B, 0x46, 0x3E, 0x02, 0x3C, 0x40,
        // h
        0x40, 0x40, 0x7C, 0x42, 0x42, 0x42, 0x42, 0x00,
        // i
        0x08, 0x00, 0x18, 0x08, 0x08, 0x08, 0x1C, 0x00,
        // j
        0x04, 0x00, 0x0C, 0x04, 0x04, 0x04, 0x44, 0x38,
        // k
        0x40, 0x40, 0x44, 0x48, 0x70, 0x48, 0x44, 0x00,
        // l
        0x18, 0x08, 0x08, 0x08, 0x08, 0x08, 0x1C, 0x00,
        // m
        0x00, 0x00, 0x7C, 0x54, 0x54, 0x54, 0x54, 0x00,
        // n
        0x00, 0x00, 0x7C, 0x42, 0x42, 0x42, 0x42, 0x00,
        // o
        0x00, 0x00, 0x3C, 0x42, 0x42, 0x42, 0x3C, 0x00,
        // p
        0x00, 0x00, 0x7C, 0x42, 0x42, 0x42, 0x7C, 0x40,
        // q
        0x00, 0x00, 0x3E, 0x42, 0x42, 0x42, 0x3E, 0x02,
        // r
        0x00, 0x00, 0x5C, 0x62, 0x40, 0x40, 0x40, 0x00,
        // s
        0x00, 0x00, 0x3E, 0x40, 0x3C, 0x02, 0x7C, 0x00,
        // t
        0x20, 0x20, 0x7C, 0x20, 0x20, 0x22, 0x1C, 0x00,
        // u
        0x00, 0x00, 0x42, 0x42, 0x42, 0x46, 0x3B, 0x00,
        // v
        0x00, 0x00, 0x42, 0x42, 0x24, 0x24, 0x18, 0x00,
        // w
        0x00, 0x00, 0x41, 0x49, 0x55, 0x63, 0x22, 0x00,
        // x
        0x00, 0x00, 0x42, 0x24, 0x18, 0x24, 0x42, 0x00,
        // y
        0x00, 0x00, 0x42, 0x42, 0x42, 0x3E, 0x02, 0x3C,
        // z
        0x00, 0x00, 0x7E, 0x04, 0x08, 0x10, 0x7E, 0x00,
        // {
        0x0C, 0x10, 0x10, 0x20, 0x10, 0x10, 0x0C, 0x00,
        // |
        0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x00,
        // }
        0x30, 0x08, 0x08, 0x04, 0x08, 0x08, 0x30, 0x00,
        // ~
        0x00, 0x00, 0x32, 0x4C, 0x00, 0x00, 0x00, 0x00,
        // DEL (127)
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
    };

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
    /// Creates a default 8x8 font in memory using a built-in bitmap.
    /// </summary>
    public static BitmapFont CreateDefault(GL gl, ILogger<BitmapFont> logger)
    {
        ArgumentNullException.ThrowIfNull(gl);
        ArgumentNullException.ThrowIfNull(logger);

        var font = new BitmapFont(logger);
        font.LineHeight = 8;

        // Generate 128x64 pixels texture
        var width = 128;
        var height = 64;
        var pixels = new byte[width * height * 4];

        // Fill pixels array based on FontBitmaps
        for (int i = 0; i < 96; i++)
        {
            var gridX = i % 16;
            var gridY = i / 16;

            var pixelStartX = gridX * 8;
            var pixelStartY = gridY * 8;

            for (int row = 0; row < 8; row++)
            {
                byte rowVal = FontBitmaps[i * 8 + row];
                for (int col = 0; col < 8; col++)
                {
                    bool set = (rowVal & (1 << (7 - col))) != 0;
                    int px = pixelStartX + col;
                    int py = pixelStartY + row;
                    int index = (py * width + px) * 4;

                    if (set)
                    {
                        pixels[index] = 255;     // R
                        pixels[index + 1] = 255; // G
                        pixels[index + 2] = 255; // B
                        pixels[index + 3] = 255; // A
                    }
                    else
                    {
                        pixels[index] = 0;
                        pixels[index + 1] = 0;
                        pixels[index + 2] = 0;
                        pixels[index + 3] = 0; // Transparent
                    }
                }
            }
        }

        // Upload to GL
        uint tex = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2D, tex);
        unsafe
        {
            fixed (byte* ptr = pixels)
            {
                gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)width, (uint)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }
        }

        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        gl.BindTexture(TextureTarget.Texture2D, 0);

        font._atlasTextureHandle = (int)tex;
        font._atlasWidth = width;
        font._atlasHeight = height;

        // Register font characters
        for (int i = 0; i < 96; i++)
        {
            var charCode = 32 + i;
            var gridX = i % 16;
            var gridY = i / 16;

            var x = gridX * 8;
            var y = gridY * 8;

            font._characters[charCode] = new FontCharacter(
                id: charCode,
                x: x,
                y: y,
                width: 8,
                height: 8,
                offsetX: 0,
                offsetY: 0,
                advanceX: 8,
                textureWidth: width,
                textureHeight: height);
        }

        return font;
    }

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
