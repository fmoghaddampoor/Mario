namespace MarioEngine.Core.Graphics;

using System;
using System.IO;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains unload and reload methods for <see cref="ShaderManager"/>.
/// </summary>
public sealed partial class ShaderManager
{
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
