namespace MarioEngine.Desktop;

using System;
using System.IO;
using Silk.NET.OpenGL;

/// <summary>
/// Contains shader loading for the SplashScreen.
/// Shaders are loaded from files in the Shaders/ directory at runtime.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>Directory containing shader source files relative to the executable.</summary>
    private const string ShaderDir = "Shaders";

    private static string ShaderPath(string filename) => Path.Combine(AppContext.BaseDirectory, ShaderDir, filename);

    /// <summary>
    /// Creates the nebula shader program (texture + UV drift).
    /// </summary>
    private static uint CreateShaderProgram(GL gl)
    {
        return ShaderLoader.LoadProgram(gl, ShaderPath("splash.vert"), ShaderPath("splash.frag"));
    }

    /// <summary>
    /// Creates the star pulsing shader program.
    /// </summary>
    private static uint CreateStarProgram(GL gl)
    {
        return ShaderLoader.LoadProgram(gl, ShaderPath("splash.vert"), ShaderPath("star.frag"));
    }

    /// <summary>
    /// Creates a pure passthrough shader program (no uTime, no effects).
    /// Used for the text overlay layer to avoid any UV or pulse artifacts.
    /// </summary>
    private static uint CreatePassthroughProgram(GL gl)
    {
        return ShaderLoader.LoadProgram(gl, ShaderPath("splash.vert"), ShaderPath("passthrough.frag"));
    }
}
