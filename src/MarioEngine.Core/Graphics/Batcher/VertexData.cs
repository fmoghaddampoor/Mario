namespace MarioEngine.Core.Graphics;

using System.Numerics;

/// <summary>
/// Vertex data structure for a single sprite vertex.
/// Contains position, texture coordinate, and color. Matches the shader input layout.
/// Size: 20 bytes (8 + 8 + 4) — ideal for efficient batching.
/// Public fields are intentional for performance (struct is used as a direct memory buffer).
/// </summary>
public struct VertexData
{
    /// <summary>Position in clip space (x, y).</summary>
    public Vector2 Position;

    /// <summary>Texture coordinate (u, v).</summary>
    public Vector2 TexCoord;

    /// <summary>Vertex color as RGBA packed into 4 bytes.</summary>
    public uint Color;

    /// <summary>
    /// Initializes a new instance of the <see cref="VertexData"/> struct.
    /// </summary>
    /// <param name="position">Vertex position in clip space.</param>
    /// <param name="texCoord">Texture coordinate (u, v).</param>
    /// <param name="color">RGBA color packed as a uint.</param>
    public VertexData(Vector2 position, Vector2 texCoord, uint color)
    {
        Position = position;
        TexCoord = texCoord;
        Color = color;
    }
}
