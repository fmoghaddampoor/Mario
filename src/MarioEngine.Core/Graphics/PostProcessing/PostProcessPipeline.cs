namespace MarioEngine.Core.Graphics.PostProcessing;

using System;
using System.Collections.Generic;
using Silk.NET.OpenGL;

/// <summary>
/// Ordered post-processing pipeline. Manages a list of passes, each with enable/disable.
/// Renders the scene through each enabled pass in sequence.
/// </summary>
public sealed class PostProcessPipeline : IDisposable
{
    private readonly GL _gl;
    private readonly FrameBuffer _mainFb;
    private readonly FrameBuffer _swapFb;
    private readonly List<PassEntry> _passes;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostProcessPipeline"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="width">Render width in pixels.</param>
    /// <param name="height">Render height in pixels.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public PostProcessPipeline(GL gl, int width, int height)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _mainFb = new FrameBuffer(gl, width, height);
        _swapFb = new FrameBuffer(gl, width, height);
        _passes = new List<PassEntry>();
    }

    /// <summary>Gets the main framebuffer where the scene should be rendered.</summary>
    public FrameBuffer MainFramebuffer => _mainFb;

    /// <summary>Gets the number of enabled passes.</summary>
    public int EnabledPassCount { get; private set; }

    /// <summary>Gets the list of pass names for debug display.</summary>
    public IReadOnlyList<string> PassNames => _passes.ConvertAll(p => p.Name);

    /// <summary>
    /// Adds a pass to the pipeline. Passes execute in the order they are added.
    /// </summary>
    /// <param name="name">Display name for debugging.</param>
    /// <param name="applyAction">Function that applies the pass.</param>
    /// <param name="enabled">Whether the pass starts enabled.</param>
    public void AddPass(string name, Action<GL, uint, FrameBuffer> applyAction, bool enabled = true)
    {
        _passes.Add(new PassEntry { Name = name, Enabled = enabled, Apply = applyAction });
        if (enabled)
        {
            EnabledPassCount++;
        }
    }

    /// <summary>
    /// Enables or disables a pass by name.
    /// </summary>
    /// <param name="name">Name of the pass to toggle.</param>
    /// <param name="enabled">Whether to enable.</param>
    public void SetPassEnabled(string name, bool enabled)
    {
        for (var i = 0; i < _passes.Count; i++)
        {
            if (_passes[i].Name != name)
            {
                continue;
            }

            var entry = _passes[i];
            if (entry.Enabled != enabled)
            {
                entry.Enabled = enabled;
                _passes[i] = entry;
                EnabledPassCount += enabled ? 1 : -1;
            }

            return;
        }
    }

    /// <summary>
    /// Executes all enabled passes in order.
    /// Call this after rendering the scene to the main framebuffer.
    /// The final result is rendered to the screen (default framebuffer).
    /// </summary>
    public void Execute()
    {
        var input = _mainFb.TextureHandle;
        FrameBuffer? output = null;

        for (var i = 0; i < _passes.Count; i++)
        {
            if (!_passes[i].Enabled)
            {
                continue;
            }

            var isLast = true;
            for (var j = i + 1; j < _passes.Count; j++)
            {
                if (_passes[j].Enabled)
                {
                    isLast = false;
                    break;
                }
            }

            if (isLast)
            {
                _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                _passes[i].Apply(_gl, input, null!);
            }
            else
            {
                output = (input == _mainFb.TextureHandle) ? _swapFb : _mainFb;
                _passes[i].Apply(_gl, input, output);
                input = output.TextureHandle;
            }
        }

        _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    /// <summary>Releases GPU resources.</summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _mainFb.Dispose();
            _swapFb.Dispose();
            _disposed = true;
        }
    }

    private struct PassEntry
    {
        public string Name;
        public bool Enabled;
        public Action<GL, uint, FrameBuffer> Apply;
    }
}
