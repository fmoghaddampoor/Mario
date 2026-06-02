namespace MarioEngine.Core.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Preprocesses GLSL shader source files before compilation.
/// Supports #include directives for modular shader authoring.
/// Tracks included files to prevent circular dependencies.
/// </summary>
internal static partial class ShaderPreprocessor
{
    /// <summary>Tracks files being included to detect circular references.</summary>
    private static readonly HashSet<string> _included = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>Regex matching #include \"filename\" directives.</summary>
    [GeneratedRegex("#include\\s+\"([^\"]+)\"")]
    private static partial Regex IncludePattern();

    /// <summary>
    /// Processes shader source, resolving all #include directives recursively.
    /// </summary>
    /// <param name="sourcePath">Path to the shader source file.</param>
    /// <returns>Processed shader source with includes inlined.</returns>
    /// <exception cref="FileNotFoundException">Thrown if a referenced include is not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown on circular includes.</exception>
    internal static string Process(string sourcePath)
    {
        _included.Clear();
        return ProcessFile(sourcePath);
    }

    /// <summary>Processes a single shader file, resolving its includes.</summary>
    private static string ProcessFile(string filePath)
    {
        var fullPath = Path.GetFullPath(filePath);
        if (!_included.Add(fullPath))
        {
            throw new InvalidOperationException($"Circular include detected: {filePath}");
        }

        var dir = Path.GetDirectoryName(fullPath)!;
        var lines = File.ReadAllLines(fullPath);
        var result = new List<string>();

        foreach (var line in lines)
        {
            var match = IncludePattern().Match(line);
            if (match.Success)
            {
                var includePath = Path.Combine(dir, match.Groups[1].Value);
                result.Add(ProcessFile(includePath));
            }
            else
            {
                result.Add(line);
            }
        }

        return string.Join('\n', result);
    }
}
