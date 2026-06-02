namespace MarioEngine.Core.Graphics;

using System;
using Silk.NET.OpenGL;

/// <summary>
/// Contains the internal flush implementation for <see cref="SpriteBatcher"/>.
/// Manages GPU buffer upload and draw call dispatch.
/// </summary>
public sealed partial class SpriteBatcher
{
    /// <summary>
    /// Flushes all pending batches to the GPU.
    /// Uploads vertex data to the VBO and issues indexed draw calls per batch.
    /// </summary>
    private unsafe void InternalFlush()
    {
        if (_vertexCount == 0)
        {
            return;
        }

        if (_shaderProgram != 0)
        {
            _gl.UseProgram(_shaderProgram);
        }

        _gl.BindVertexArray(_vao);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        fixed (VertexData* ptr = _vertexBuffer)
        {
            _gl.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (nuint)(_vertexCount * VertexStride), ptr);
        }

        foreach (var batch in _batches)
        {
            _gl.BindTexture(TextureTarget.Texture2D, batch.TextureId);
            var count = (uint)((batch.VertexCount / VerticesPerQuad) * IndicesPerQuad);
            var offset = (void*)((batch.StartVertex / VerticesPerQuad) * IndicesPerQuad * sizeof(ushort));
            _gl.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedShort, offset);
        }

        _batches.Clear();
        _vertexCount = 0;
        _batchStartVertex = 0;
        _currentTexture = 0;
    }
}
