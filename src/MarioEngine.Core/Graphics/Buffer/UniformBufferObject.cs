namespace MarioEngine.Core.Graphics.Buffer;

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;

/// <summary>
/// Manages a Uniform Buffer Object (UBO) for per-frame shader data.
/// Reduces the number of individual uniform uploads by batching
/// common data (projection, view, time) into a single buffer.
/// </summary>
#pragma warning disable CA1812 // Instantiated by render pipeline
internal sealed class UniformBufferObject : IDisposable
#pragma warning restore CA1812
{
    /// <summary>OpenGL context.</summary>
    private readonly GL _gl;

    /// <summary>OpenGL buffer handle for the UBO.</summary>
    private readonly uint _ubo;

    /// <summary>Binding point index for this UBO.</summary>
    private readonly uint _bindingPoint;

    /// <summary>Size of the UBO data in bytes.</summary>
    private readonly int _size;

    /// <summary>Backing data for the UBO, uploaded on flush.</summary>
    private PerFrameData _data;

    /// <summary>True if the data has been modified since last upload.</summary>
    private bool _dirty;

    /// <summary>True after disposal.</summary>
    private bool _disposed;

    /// <summary>Initializes a new instance of the <see cref="UniformBufferObject"/> class.</summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="bindingPoint">Binding point index for this UBO.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public UniformBufferObject(GL gl, uint bindingPoint = 0)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _bindingPoint = bindingPoint;
        _size = Marshal.SizeOf<PerFrameData>();
        _ubo = gl.GenBuffer();
        gl.BindBuffer(BufferTargetARB.UniformBuffer, _ubo);

        unsafe
        {
            gl.BufferData(BufferTargetARB.UniformBuffer, (nuint)_size, in nint.Zero, BufferUsageARB.DynamicDraw);
        }

        gl.BindBufferBase(BufferTargetARB.UniformBuffer, bindingPoint, _ubo);
        gl.BindBuffer(BufferTargetARB.UniformBuffer, 0);
    }

    /// <summary>Gets or sets the combined projection-view matrix.</summary>
    public Matrix4x4 ProjectionView
    {
        get => _data.ProjectionView;
        set
        {
            _data.ProjectionView = value;
            _dirty = true;
        }
    }

    /// <summary>Gets or sets the elapsed game time in seconds.</summary>
    public float Time
    {
        get => _data.Time;
        set
        {
            _data.Time = value;
            _dirty = true;
        }
    }

    /// <summary>Gets or sets the screen resolution as (width, height).</summary>
    public Vector2 Resolution
    {
        get => _data.Resolution;
        set
        {
            _data.Resolution = value;
            _dirty = true;
        }
    }

    /// <summary>
    /// Uploads the UBO data to the GPU if it has been modified.
    /// Call once per frame after updating all values.
    /// </summary>
    public void Flush()
    {
        if (!_dirty)
        {
            return;
        }

        _dirty = false;
        _gl.BindBuffer(BufferTargetARB.UniformBuffer, _ubo);
        unsafe
        {
            _gl.BufferSubData(BufferTargetARB.UniformBuffer, 0, (nuint)_size, ref _data);
        }

        _gl.BindBuffer(BufferTargetARB.UniformBuffer, 0);
    }

    /// <summary>Releases the UBO.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _gl.DeleteBuffer(_ubo);
    }

    /// <summary>
    /// Data layout for the per-frame uniform block.
    /// Must match the std140 layout in GLSL shaders.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct PerFrameData
    {
        /// <summary>Projection-view matrix (64 bytes, std140).</summary>
        [FieldOffset(0)]
        internal Matrix4x4 ProjectionView;

        /// <summary>Elapsed game time in seconds.</summary>
        [FieldOffset(64)]
        internal float Time;

        /// <summary>Screen resolution as (width, height).</summary>
        [FieldOffset(68)]
        internal Vector2 Resolution;

        /// <summary>Padding to meet std140 alignment.</summary>
        [FieldOffset(76)]
        private readonly float _padding;
    }
}
