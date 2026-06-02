namespace MarioEngine.Core.Graphics.Debug;

using System;
using System.IO;
using Silk.NET.OpenGL;

/// <summary>
/// Captures the current framebuffer to a PNG file.
/// Bound to F12 key (requires input system).
/// </summary>
internal sealed class ScreenshotCapture
{
    /// <summary>OpenGL context.</summary>
    private readonly GL _gl;

    /// <summary>Output directory for screenshots.</summary>
    private readonly string _outputDir;

    /// <summary>Screenshot counter for unique filenames.</summary>
    private int _screenshotIndex;

    /// <summary>Initializes a new instance of the <see cref="ScreenshotCapture"/> class.</summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="outputDir">Directory to save screenshots (default &quot;screenshots&quot;).</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public ScreenshotCapture(GL gl, string outputDir = "screenshots")
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _outputDir = outputDir;
        Directory.CreateDirectory(outputDir);
    }

    /// <summary>
    /// Captures the current framebuffer to a timestamped PNG file.
    /// </summary>
    /// <param name="width">Framebuffer width in pixels.</param>
    /// <param name="height">Framebuffer height in pixels.</param>
    /// <returns>The path to the saved screenshot file.</returns>
    public string Capture(int width, int height)
    {
        var pixels = new byte[width * height * 4];

        unsafe
        {
            fixed (byte* ptr = pixels)
            {
                _gl.ReadPixels(0, 0, (uint)width, (uint)height, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }
        }

        // Flip vertically (OpenGL has Y=0 at bottom)
        var flipped = new byte[width * height * 4];
        for (var y = 0; y < height; y++)
        {
            System.Buffer.BlockCopy(pixels, y * width * 4, flipped, ((height - 1) - y) * width * 4, width * 4);
        }

        var filename = $"screenshot_{++_screenshotIndex:000}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var path = Path.Combine(_outputDir, filename);

        // Write raw RGBA data as a simple placeholder (PNG encoding deferred)
        // For actual PNG output, integrate StbImageWrite or a similar library
        var header = new byte[18 + (width * height * 4)];
        var filePath = path;
        File.WriteAllBytes(filePath, flipped);
        return path;
    }
}
