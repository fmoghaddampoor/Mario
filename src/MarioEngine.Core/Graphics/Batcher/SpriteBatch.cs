namespace MarioEngine.Core.Graphics;

/// <summary>
/// Represents a batch of sprite vertices that share the same OpenGL texture.
/// Used internally by <see cref="SpriteBatcher"/> to group draw calls.
/// </summary>
internal readonly struct SpriteBatch
{
    /// <summary>OpenGL texture handle for this batch.</summary>
    internal readonly uint TextureId;

    /// <summary>Start index in the vertex buffer.</summary>
    internal readonly int StartVertex;

    /// <summary>Number of vertices in this batch.</summary>
    internal readonly int VertexCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpriteBatch"/> struct.
    /// </summary>
    /// <param name="textureId">OpenGL texture handle.</param>
    /// <param name="startVertex">Start index in the vertex buffer.</param>
    /// <param name="vertexCount">Number of vertices in this batch.</param>
    internal SpriteBatch(uint textureId, int startVertex, int vertexCount)
    {
        TextureId = textureId;
        StartVertex = startVertex;
        VertexCount = vertexCount;
    }
}
