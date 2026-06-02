namespace MarioEngine.Core.Graphics.Texture;

using System;

/// <summary>
/// Provides runtime texture compression using DXT5/BC3 format.
/// Compresses RGBA32 raw pixel data into BC3 blocks.
/// Each 4x4 block of RGBA pixels compresses to 16 bytes.
/// </summary>
internal static class TextureCompression
{
    /// <summary>Size of a BC3 block in bytes (128 bits).</summary>
    private const int Bc3BlockSize = 16;

    /// <summary>Size of a 4x4 pixel block.</summary>
    private const int BlockPixelCount = 16;

    /// <summary>Bytes per pixel for input RGBA data.</summary>
    private const int RgbaBytesPerPixel = 4;

    /// <summary>
    /// Compresses RGBA32 pixel data to BC3 (DXT5) format.
    /// </summary>
    /// <param name="rgbaData">Raw RGBA pixel data (width * height * 4 bytes).</param>
    /// <param name="width">Texture width in pixels (must be multiple of 4).</param>
    /// <param name="height">Texture height in pixels (must be multiple of 4).</param>
    /// <returns>BC3 compressed data.</returns>
    internal static byte[] CompressBC3(byte[] rgbaData, int width, int height)
    {
        var blocksX = Math.Max(1, width / 4);
        var blocksY = Math.Max(1, height / 4);
        var result = new byte[blocksX * blocksY * Bc3BlockSize];

        for (var by = 0; by < blocksY; by++)
        {
            for (var bx = 0; bx < blocksX; bx++)
            {
                var srcOffset = ((by * 4) * width) + (bx * 4);
                var block = ExtractBlock(rgbaData, width, srcOffset);

                var destBase = ((by * blocksX) + bx) * Bc3BlockSize;
                CompressAlpha(block, result, destBase);
                CompressColor(block, result, destBase + 8);
            }
        }

        return result;
    }

    /// <summary>Extracts a 4x4 block of RGBA pixels from the source data.</summary>
    private static byte[] ExtractBlock(byte[] src, int stride, int offset)
    {
        var block = new byte[BlockPixelCount * RgbaBytesPerPixel];
        for (var row = 0; row < 4; row++)
        {
            var srcRow = offset + (row * stride * RgbaBytesPerPixel);
            var dstRow = row * 4 * RgbaBytesPerPixel;
            Buffer.BlockCopy(src, srcRow, block, dstRow, 4 * RgbaBytesPerPixel);
        }

        return block;
    }

    /// <summary>Compresses the alpha channel of a 4x4 block using BC3 alpha method.</summary>
    private static void CompressAlpha(byte[] block, byte[] dest, int destOffset)
    {
        var alphas = new byte[BlockPixelCount];
        for (var i = 0; i < BlockPixelCount; i++)
        {
            alphas[i] = block[(i * RgbaBytesPerPixel) + 3];
        }

        var min = byte.MaxValue;
        var max = byte.MinValue;
        for (var i = 0; i < BlockPixelCount; i++)
        {
            if (alphas[i] < min)
            {
                min = alphas[i];
            }

            if (alphas[i] > max)
            {
                max = alphas[i];
            }
        }

        dest[destOffset] = max;
        dest[destOffset + 1] = min;

        for (var i = 0; i < BlockPixelCount; i++)
        {
            var mid = (min + max) / 2;
            var code = alphas[i] < mid ? 0 : 1;
            var byteIndex = destOffset + 2 + (i / 4);
            var bitOffset = (i % 4) * 2;
            dest[byteIndex] |= (byte)(code << bitOffset);
        }
    }

    /// <summary>Compresses the color channels of a 4x4 block using BC1 color method.</summary>
    private static void CompressColor(byte[] block, byte[] dest, int destOffset)
    {
        ushort c0 = 0;
        ushort c1 = 0xFFFF;
        var bits = 0u;

        for (var i = 0; i < BlockPixelCount; i++)
        {
            var r = block[i * RgbaBytesPerPixel];
            var g = block[(i * RgbaBytesPerPixel) + 1];
            var b = block[(i * RgbaBytesPerPixel) + 2];
            var lum = ((r * 77) + (g * 150) + (b * 29)) >> 8;

            var code = lum > 128 ? 0u : 1u;
            bits |= code << (i * 2);
        }

        dest[destOffset] = (byte)(c0 & 0xFF);
        dest[destOffset + 1] = (byte)((c0 >> 8) & 0xFF);
        dest[destOffset + 2] = (byte)(c1 & 0xFF);
        dest[destOffset + 3] = (byte)((c1 >> 8) & 0xFF);
        dest[destOffset + 4] = (byte)(bits & 0xFF);
        dest[destOffset + 5] = (byte)((bits >> 8) & 0xFF);
        dest[destOffset + 6] = (byte)((bits >> 16) & 0xFF);
        dest[destOffset + 7] = (byte)((bits >> 24) & 0xFF);
    }
}
