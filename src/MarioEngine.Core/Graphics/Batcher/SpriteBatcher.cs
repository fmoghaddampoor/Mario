namespace MarioEngine.Core.Graphics;

using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// Batches sprite draw calls by texture to minimize OpenGL state changes and draw calls.
/// Groups quads by texture handle and flushes batches to the GPU.
/// </summary>
public sealed partial class SpriteBatcher : IDisposable
{
    /// <summary>Maximum number of vertices the batcher can hold per frame.</summary>
    private const int MaxVertices = 65536;

    /// <summary>Number of vertices per sprite quad.</summary>
    private const int VerticesPerQuad = 4;

    /// <summary>Byte size of a single vertex (position 8 + texcoord 8 + color 4).</summary>
    private const int VertexStride = 20;

    /// <summary>Number of indices per quad (2 triangles = 6 indices).</summary>
    private const int IndicesPerQuad = 6;

    /// <summary>OpenGL context for GPU operations.</summary>
    private readonly GL _gl;

    /// <summary>Logger for debug output.</summary>
    private readonly ILogger<SpriteBatcher> _logger;

    /// <summary>Pre-allocated vertex buffer for collecting sprite vertices each frame.</summary>
    private readonly VertexData[] _vertexBuffer;

    /// <summary>Pre-filled index buffer with quad indices (0,1,2,2,3,0 repeated).</summary>
    private readonly ushort[] _indexBuffer;

    /// <summary>List of texture batches accumulated during the current frame.</summary>
    private readonly List<SpriteBatch> _batches;

    /// <summary>OpenGL vertex array object handle.</summary>
    private readonly uint _vao;

    /// <summary>OpenGL vertex buffer object handle for dynamic vertex data.</summary>
    private readonly uint _vbo;

    /// <summary>OpenGL index buffer object handle for static quad indices.</summary>
    private readonly uint _ibo;

    /// <summary>Shader program handle to bind before drawing. 0 = use fixed function (not available in core profile).</summary>
    private uint _shaderProgram;

    /// <summary>Gets or sets the shader program handle to bind before drawing.</summary>
    public uint ShaderProgram
    {
        get => _shaderProgram;
        set => _shaderProgram = value;
    }

    /// <summary>True after GPU resources have been allocated.</summary>
    private readonly bool _initialized;

    /// <summary>Total vertices queued so far in the current frame.</summary>
    private int _vertexCount;

    /// <summary>Texture handle of the current active batch.</summary>
    private uint _currentTexture;

    /// <summary>Vertex buffer index where the current batch started.</summary>
    private int _batchStartVertex;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpriteBatcher"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl or logger is null.</exception>
    public SpriteBatcher(GL gl, ILogger<SpriteBatcher> logger)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _vertexBuffer = new VertexData[MaxVertices];
        _indexBuffer = new ushort[MaxVertices / VerticesPerQuad * IndicesPerQuad];
        _batches = new List<SpriteBatch>(128);

        // Pre-fill index buffer with quad indices
        var maxQuads = MaxVertices / VerticesPerQuad;
        for (var i = 0; i < maxQuads; i++)
        {
            var baseVertex = (ushort)(i * VerticesPerQuad);
            var idx = i * IndicesPerQuad;
            _indexBuffer[idx + 0] = (ushort)(baseVertex + 0);
            _indexBuffer[idx + 1] = (ushort)(baseVertex + 1);
            _indexBuffer[idx + 2] = (ushort)(baseVertex + 2);
            _indexBuffer[idx + 3] = (ushort)(baseVertex + 2);
            _indexBuffer[idx + 4] = (ushort)(baseVertex + 3);
            _indexBuffer[idx + 5] = (ushort)(baseVertex + 0);
        }

        // Create GPU resources
        var maxIndices = maxQuads * IndicesPerQuad;

        _vao = gl.GenVertexArray();
        _vbo = gl.GenBuffer();
        _ibo = gl.GenBuffer();

        gl.BindVertexArray(_vao);

        gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        unsafe
        {
            gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(MaxVertices * VertexStride), null, BufferUsageARB.DynamicDraw);
        }

        gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ibo);
        unsafe
        {
            fixed (ushort* ptr = _indexBuffer)
            {
                gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(maxIndices * sizeof(ushort)), ptr, BufferUsageARB.StaticDraw);
            }
        }

        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, VertexStride, 0);

        gl.EnableVertexAttribArray(1);
        gl.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, VertexStride, 8);

        gl.EnableVertexAttribArray(2);
        gl.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, VertexStride, 16);

        gl.BindVertexArray(0);

        _initialized = true;
        _logger.LogDebug("SpriteBatcher initialized");
    }

    /// <summary>
    /// Begins a new frame. Resets batch state. Call once per frame before any Draw calls.
    /// </summary>
    public void Begin()
    {
        _vertexCount = 0;
        _currentTexture = 0;
        _batchStartVertex = 0;
        _batches.Clear();
    }

    /// <summary>
    /// Draws a sprite quad with the specified texture, position, color, and layer.
    /// </summary>
    /// <param name="texture">OpenGL texture handle.</param>
    /// <param name="x">X position in clip space (-1 to 1).</param>
    /// <param name="y">Y position in clip space (-1 to 1).</param>
    /// <param name="width">Width in clip space.</param>
    /// <param name="height">Height in clip space.</param>
    /// <param name="color">RGBA color packed as uint.</param>
    /// <param name="layer">Render layer for sorting (higher = on top).</param>
    /// <param name="u1">Texture coordinate left.</param>
    /// <param name="v1">Texture coordinate top.</param>
    /// <param name="u2">Texture coordinate right.</param>
    /// <param name="v2">Texture coordinate bottom.</param>
    public void Draw(
        uint texture,
        float x,
        float y,
        float width,
        float height,
        uint color,
        float layer,
        float u1 = 0f,
        float v1 = 0f,
        float u2 = 1f,
        float v2 = 1f)
    {
        if (_vertexCount + VerticesPerQuad > MaxVertices)
        {
            InternalFlush();
        }

        if (texture != _currentTexture && _vertexCount > _batchStartVertex)
        {
            _batches.Add(new SpriteBatch(
                _currentTexture,
                _batchStartVertex,
                _vertexCount - _batchStartVertex));
            _batchStartVertex = _vertexCount;
        }

        _currentTexture = texture;
        _vertexBuffer[_vertexCount++] = new VertexData(new Vector2(x, y), new Vector2(u1, v2), color);
        _vertexBuffer[_vertexCount++] = new VertexData(new Vector2(x + width, y), new Vector2(u2, v2), color);
        _vertexBuffer[_vertexCount++] = new VertexData(new Vector2(x + width, y + height), new Vector2(u2, v1), color);
        _vertexBuffer[_vertexCount++] = new VertexData(new Vector2(x, y + height), new Vector2(u1, v1), color);
    }

    /// <summary>
    /// Ends the frame. Flushes all remaining batches to the GPU. Call once per frame after all Draw calls.
    /// </summary>
    public void End()
    {
        if (_vertexCount > _batchStartVertex)
        {
            _batches.Add(new SpriteBatch(
                _currentTexture,
                _batchStartVertex,
                _vertexCount - _batchStartVertex));
        }

        InternalFlush();
    }

    /// <summary>Releases GPU resources.</summary>
    public void Dispose()
    {
        if (_initialized)
        {
            _gl.DeleteVertexArray(_vao);
            _gl.DeleteBuffer(_vbo);
            _gl.DeleteBuffer(_ibo);
        }
    }
}
