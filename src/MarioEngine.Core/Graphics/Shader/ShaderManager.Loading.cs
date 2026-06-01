namespace MarioEngine.Core.Graphics;

using System.IO;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains shader loading and compilation methods for <see cref="ShaderManager"/>.
/// </summary>
public sealed partial class ShaderManager
{
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
}
