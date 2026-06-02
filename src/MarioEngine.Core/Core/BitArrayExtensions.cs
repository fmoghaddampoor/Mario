namespace MarioEngine.Core.Core;

/// <summary>Efficient flag bit operations on integers.</summary>
internal static class BitArrayExtensions
{
    public static bool IsBitSet(this int flags, int bit) => (flags & (1 << bit)) != 0;

    public static int SetBit(this int flags, int bit, bool value)
    {
        if (value) return flags | (1 << bit);
        return flags & ~(1 << bit);
    }
}
