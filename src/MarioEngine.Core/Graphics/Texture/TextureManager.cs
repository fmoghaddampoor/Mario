namespace MarioEngine.Core.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenGL;
using StbImageSharp;

/// <summary>
/// Manages loading, caching, and unloading of OpenGL textures.
/// Supports PNG loading, mipmap generation, atlas loading, and async loading.
/// </summary>
public sealed partial class TextureManager
{
    /// <summary>OpenGL context for GPU texture uploads.</summary>
    private readonly GL _gl;

    /// <summary>Logger for texture loading events.</summary>
    private readonly ILogger<TextureManager> _logger;

    /// <summary>Texture cache keyed by file path, using weak references for auto-cleanup.</summary>
    private readonly Dictionary<string, WeakReference<Texture2D>> _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureManager"/> class.
    /// </summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <param name="logger">Logger instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl or logger is null.</exception>
    public TextureManager(GL gl, ILogger<TextureManager> logger)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = new Dictionary<string, WeakReference<Texture2D>>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Loads a PNG texture from disk. Returns cached instance if already loaded.
    /// </summary>
    /// <param name="filePath">Path to the PNG file.</param>
    /// <returns>A Texture2D instance.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    public Texture2D LoadTexture(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Texture file not found: {filePath}");
        }

        var fullPath = Path.GetFullPath(filePath);

        if (_cache.TryGetValue(fullPath, out var weakRef) && weakRef.TryGetTarget(out var cached))
        {
            cached.AddRef();
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Cache hit: {Path}", fullPath);
            }

            return cached;
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Loading texture: {Path}", fullPath);
        }

        ImageResult image;
        using (var stream = File.OpenRead(fullPath))
        {
            image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }

        var handle = UploadToGpu(image);

        var texture = new Texture2D(_gl, handle, image.Width, image.Height, fullPath);
        _cache[fullPath] = new WeakReference<Texture2D>(texture);
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Loaded texture: {Path} ({Width}x{Height})", fullPath, image.Width, image.Height);
        }

        return texture;
    }

    /// <summary>
    /// Loads a PNG texture from a byte array (e.g. embedded resources).
    /// </summary>
    /// <param name="data">Raw PNG bytes.</param>
    /// <param name="name">Cache key name for this texture.</param>
    /// <returns>A Texture2D instance.</returns>
    public Texture2D LoadTextureFromBytes(byte[] data, string name)
    {
        if (_cache.TryGetValue(name, out var weakRef) && weakRef.TryGetTarget(out var cached))
        {
            cached.AddRef();
            return cached;
        }

        ImageResult image;
        using (var stream = new MemoryStream(data))
        {
            image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }

        var handle = UploadToGpu(image);
        var texture = new Texture2D(_gl, handle, image.Width, image.Height, name);
        _cache[name] = new WeakReference<Texture2D>(texture);
        return texture;
    }

    /// <summary>
    /// Unloads a texture from the GPU and removes it from the cache.
    /// Only disposes if the texture has zero references.
    /// </summary>
    /// <param name="texture">The texture to unload. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if texture is null.</exception>
    public void UnloadTexture(Texture2D texture)
    {
        ArgumentNullException.ThrowIfNull(texture);
        texture.Release();
        if (texture.ReferenceCount <= 0 && _cache.TryGetValue(texture.Path, out var weakRef))
        {
            if (!weakRef.TryGetTarget(out var cached) || cached == texture)
            {
                _cache.Remove(texture.Path);
            }
        }
    }

    /// <summary>
    /// Loads a texture atlas from a JSON metadata file and a PNG atlas image.
    /// </summary>
    /// <param name="atlasPath">Path to the atlas JSON metadata file.</param>
    /// <param name="imagePath">Path to the atlas PNG image.</param>
    /// <returns>A TextureAtlas instance.</returns>
    public TextureAtlas LoadAtlas(string atlasPath, string imagePath)
    {
        var texture = LoadTexture(imagePath);
        var atlas = TextureAtlas.FromJson(texture, atlasPath);
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Loaded atlas: {Path} ({Count} regions)", atlasPath, atlas.RegionCount);
        }

        return atlas;
    }

    /// <summary>
    /// Unloads all textures from the cache (used on scene unload).
    /// </summary>
    public void UnloadAll()
    {
        foreach (var kvp in _cache)
        {
            if (kvp.Value.TryGetTarget(out var texture))
            {
                texture.Dispose();
            }
        }

        _cache.Clear();
        _logger.LogInformation("All textures unloaded");
    }

    /// <summary>
    /// Asynchronously loads a texture on a background thread.
    /// The PNG is decoded off the main thread; GPU upload happens synchronously.
    /// </summary>
    /// <param name="filePath">Path to the PNG file.</param>
    /// <returns>A task that completes with the loaded Texture2D.</returns>
    public async Task<Texture2D> LoadTextureAsync(string filePath)
    {
        var fullPath = Path.GetFullPath(filePath);

        if (_cache.TryGetValue(fullPath, out var weakRef) && weakRef.TryGetTarget(out var cached))
        {
            cached.AddRef();
            return cached;
        }

        var image = await Task.Run(() =>
        {
            using var stream = File.OpenRead(fullPath);
            return ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }).ConfigureAwait(false);

        var handle = UploadToGpu(image);
        var texture = new Texture2D(_gl, handle, image.Width, image.Height, fullPath);
        _cache[fullPath] = new WeakReference<Texture2D>(texture);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Async loaded texture: {Path} ({Width}x{Height})", fullPath, image.Width, image.Height);
        }

        return texture;
    }
}
