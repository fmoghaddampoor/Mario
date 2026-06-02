using System.Text;

namespace MarioEngine.Core.Save;

/// <summary>Provides XOR-based encryption and Base64 encoding for save data.</summary>
internal static class SaveEncryption
{
    private static readonly byte[] Key = { 0x4D, 0x61, 0x72, 0x69, 0x6F };

    public static string Encrypt(string data)
    {
        var bytes = Encoding.UTF8.GetBytes(data);
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] ^= Key[i % Key.Length];
        return Convert.ToBase64String(bytes);
    }

    public static string Decrypt(string data)
    {
        var bytes = Convert.FromBase64String(data);
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] ^= Key[i % Key.Length];
        return Encoding.UTF8.GetString(bytes);
    }
}
