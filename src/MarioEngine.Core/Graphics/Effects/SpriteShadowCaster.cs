namespace MarioEngine.Core.Graphics.Effects;

/// <summary>Projects a sprite shadow based on light direction.</summary>
internal sealed class SpriteShadowCaster
{
    public float ShadowOffset { get; set; } = 4f;
    public float ShadowOpacity { get; set; } = 0.3f;

    public Vector2 GetShadowPosition(Vector2 spritePos, Vector2 lightDir)
    {
        return new Vector2(
            spritePos.X - lightDir.X * ShadowOffset,
            spritePos.Y - lightDir.Y * ShadowOffset);
    }
}
