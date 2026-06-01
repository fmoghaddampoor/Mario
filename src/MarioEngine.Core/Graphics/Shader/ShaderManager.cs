namespace MarioEngine.Core.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;

/// <summary>
/// Compiles, loads, caches, and manages shader programs.
/// Supports file-based shaders and hot-reload on source file changes.
/// </summary>
public sealed partial class ShaderManager
{
    private readonly GL _gl;
    private readonly ILogger<ShaderManager> _logger;
    private readonly Dictionary<string, Shader> _cache;
    private readonly Dictionary<string, FileSystemWatcher> _watchers;
    private Shader? _defaultShader;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderManager"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl or logger is null.</exception>
    public ShaderManager(GL gl, ILogger<ShaderManager> logger)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = new Dictionary<string, Shader>(StringComparer.OrdinalIgnoreCase);
        _watchers = new Dictionary<string, FileSystemWatcher>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>Gets the default textured-quad shader, loaded on first access.</summary>
    public Shader Default
    {
        get
        {
            if (_defaultShader == null)
            {
                _defaultShader = LoadShader("default", "Shader/default.vert", "Shader/default.frag");
            }

            return _defaultShader;
        }
    }
}
