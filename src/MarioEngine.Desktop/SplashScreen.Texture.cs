namespace MarioEngine.Desktop;

using System;
using System.IO;
using MarioEngine.Desktop.Resources;
using Silk.NET.OpenGL;
using StbImageSharp;

/// <summary>
/// Contains texture loading methods for the <see cref="SplashScreen"/> class.
/// </summary>
internal sealed partial class SplashScreen
{
    /// <summary>
    /// Loads a PNG image from disk and creates an OpenGL texture from it.
    /// </summary>
    /// <param name="gl">OpenGL context.</param>
    /// <param name="path">File path to the PNG image.</param>
    /// <returns>Handle to the created OpenGL texture.</returns>
    private static uint LoadTexture(GL gl, string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(string.Format(Resources.Strings.Splash_NotFound, path));
        }

        ImageResult image;
        using (var stream = File.OpenRead(path))
        {
            image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }

        var handle = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2D, handle);

        gl.TexImage2D(
            TextureTarget.Texture2D,
            0,
            InternalFormat.Rgba,
            (uint)image.Width,
            (uint)image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            image.Data);

        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        gl.BindTexture(TextureTarget.Texture2D, 0);

        return handle;
    }
}
