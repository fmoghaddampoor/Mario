namespace MarioEngine.Core.Graphics.Effects;

/// <summary>Lookup-table based palette swapping effect.</summary>
internal sealed class PaletteSwapEffect
{
    public Color[] Palette { get; set; } = Array.Empty<Color>();
    public Color[] SwapColors { get; set; } = Array.Empty<Color>();

    public uint Apply(uint originalColor)
    {
        for (int i = 0; i < Palette.Length && i < SwapColors.Length; i++)
        {
            if (originalColor == Palette[i].ToUint())
                return SwapColors[i].ToUint();
        }
        return originalColor;
    }
}

internal sealed class Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; } = 255;

    public uint ToUint() =>
        (uint)((A << 24) | (B << 16) | (G << 8) | R);
}
