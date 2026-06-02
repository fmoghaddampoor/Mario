namespace MarioEngine.Core.Graphics.Texture;

/// <summary>Combines multiple textures into a single atlas.</summary>
internal sealed class TexturePacker
{
    public Dictionary<string, AtlasRegion> Regions { get; } = new();

    public void Pack(List<Texture2D> textures)
    {
        int x = 0;
        foreach (var tex in textures)
        {
            Regions[tex.Name] = new AtlasRegion
            {
                X = x, Y = 0,
                Width = tex.Width, Height = tex.Height
            };
            x += tex.Width;
        }
    }
}

internal sealed class AtlasRegion
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

internal sealed class Texture2D
{
    public string Name { get; set; } = "";
    public int Width { get; set; }
    public int Height { get; set; }
}
