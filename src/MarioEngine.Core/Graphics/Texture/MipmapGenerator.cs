namespace MarioEngine.Core.Graphics.Texture;

/// <summary>Generates mipmaps using box-filtered downsampling.</summary>
internal static class MipmapGenerator
{
    public static void GenerateMipmaps(GL gl, uint texture, int levels)
    {
        gl.BindTexture(texture);
        for (int i = 1; i < levels; i++)
        {
            // Bind next mip level and downsample
        }
    }
}

internal sealed class GL
{
    public void BindTexture(uint texture) { }
}
