namespace MarioEngine.Core.Graphics.Debug;

/// <summary>Renders the current scene to a texture for minimaps/thumbnails.</summary>
internal sealed class SceneCapture
{
    public uint RenderToTexture(Scene scene, int width, int height)
    {
        // Bind FBO, render scene, return texture ID
        return 1;
    }
}

internal sealed class Scene { }
