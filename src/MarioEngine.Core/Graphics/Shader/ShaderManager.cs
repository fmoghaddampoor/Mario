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
    /// <summary>OpenGL context for shader compilation.</summary>
    private readonly GL _gl;

    /// <summary>Logger for shader compilation and reload events.</summary>
    private readonly ILogger<ShaderManager> _logger;

    /// <summary>Cache of loaded shaders by name.</summary>
    private readonly Dictionary<string, Shader> _cache;

    /// <summary>File system watchers for hot-reload, keyed by shader name.</summary>
    private readonly Dictionary<string, FileSystemWatcher> _watchers;

    /// <summary>Cached default shader, loaded lazily on first access.</summary>
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
