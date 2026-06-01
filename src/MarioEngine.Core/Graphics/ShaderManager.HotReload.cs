namespace MarioEngine.Core.Graphics;

using System;
using System.IO;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains hot-reload watcher setup for <see cref="ShaderManager"/>.
/// </summary>
public sealed partial class ShaderManager
{
    /// <summary>
    /// Sets up a FileSystemWatcher to automatically reload shaders when source files change.
    /// </summary>
    /// <param name="name">Shader name for cache lookup.</param>
    /// <param name="vertexPath">Path to the vertex shader source file.</param>
    /// <param name="fragmentPath">Path to the fragment shader source file.</param>
    private void SetupHotReload(string name, string vertexPath, string fragmentPath)
    {
        try
        {
            var dir = Path.GetDirectoryName(vertexPath) ?? ".";
            var watcher = new FileSystemWatcher(dir)
            {
                Filter = "*.vert",
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true,
            };

            watcher.Changed += (s, e) =>
            {
                if (e.FullPath == vertexPath || e.FullPath == fragmentPath)
                {
                    ReloadShader(name);
                }
            };

            _watchers[name] = watcher;
        }
        catch (ArgumentException)
        {
        }
        catch (FileNotFoundException)
        {
        }
    }
}
