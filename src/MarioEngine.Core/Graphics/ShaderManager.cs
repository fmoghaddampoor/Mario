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
                _defaultShader = LoadShader("default", "Shaders/default.vert", "Shaders/default.frag");
            }

            return _defaultShader;
        }
    }

    /// <summary>
    /// Loads a shader program from vertex and fragment shader source files.
    /// Returns cached instance if already loaded. Sets up hot-reload watcher.
    /// </summary>
    /// <param name="name">Unique name for caching.</param>
    /// <param name="vertexPath">Path to vertex shader source file.</param>
    /// <param name="fragmentPath">Path to fragment shader source file.</param>
    /// <returns>A compiled and linked Shader instance.</returns>
    public Shader LoadShader(string name, string vertexPath, string fragmentPath)
    {
        if (_cache.TryGetValue(name, out var cached))
        {
            return cached;
        }

        var vertSource = File.ReadAllText(vertexPath);
        var fragSource = File.ReadAllText(fragmentPath);
        var shader = CompileAndLink(name, vertSource, fragSource);
        _cache[name] = shader;
        SetupHotReload(name, vertexPath, fragmentPath);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Loaded shader: {Name}", name);
        }

        return shader;
    }

    /// <summary>
    /// Compiles a shader program from source strings without file caching.
    /// </summary>
    /// <param name="name">Debug name for error messages.</param>
    /// <param name="vertexSource">Vertex shader GLSL source.</param>
    /// <param name="fragmentSource">Fragment shader GLSL source.</param>
    /// <returns>A compiled and linked Shader instance.</returns>
    public Shader CompileShader(string name, string vertexSource, string fragmentSource)
    {
        if (_cache.TryGetValue(name, out var cached))
        {
            return cached;
        }

        var shader = CompileAndLink(name, vertexSource, fragmentSource);
        _cache[name] = shader;
        return shader;
    }

    /// <summary>
    /// Removes a shader from the cache and disposes it.
    /// </summary>
    /// <param name="name">Shader name to unload.</param>
    public void UnloadShader(string name)
    {
        if (_cache.TryGetValue(name, out var shader))
        {
            _cache.Remove(name);
            shader.Dispose();
        }

        if (_watchers.TryGetValue(name, out var watcher))
        {
            _watchers.Remove(name);
            watcher.Dispose();
        }
    }

    /// <summary>
    /// Reloads a specific shader from its source files (called by hot-reload watcher).
    /// </summary>
    /// <param name="name">Shader name to reload.</param>
    public void ReloadShader(string name)
    {
        if (!_cache.TryGetValue(name, out var existing))
        {
            _logger.LogWarning("Cannot reload unknown shader: {Name}", name);
            return;
        }

        try
        {
            var vertSource = File.ReadAllText(existing.VertexSource);
            var fragSource = File.ReadAllText(existing.FragmentSource);
            var newShader = CompileAndLink(name, vertSource, fragSource);

            existing.Dispose();
            _cache[name] = newShader;
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Hot-reloaded shader: {Name}", name);
            }
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "Failed to read shader source for reload: {Name}", name);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to compile reloaded shader: {Name}", name);
        }
    }

    /// <summary>Unloads all shaders and clears the cache.</summary>
    public void UnloadAll()
    {
        foreach (var watcher in _watchers.Values)
        {
            watcher.Dispose();
        }

        _watchers.Clear();

        foreach (var shader in _cache.Values)
        {
            shader.Dispose();
        }

        _cache.Clear();
        _defaultShader = null;
        _logger.LogInformation("All shaders unloaded");
    }
}
