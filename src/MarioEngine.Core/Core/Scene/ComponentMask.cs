namespace MarioEngine.Core.Core.Scene;

/// <summary>Bitmask for fast entity querying by component type.</summary>
internal sealed class ComponentMask
{
    private readonly System.Collections.BitArray _bits = new(64);

    public void SetFlag<T>()
    {
        int index = ComponentTypeIndex<T>.Index;
        if (index < _bits.Length) _bits[index] = true;
    }

    public bool HasFlag<T>()
    {
        int index = ComponentTypeIndex<T>.Index;
        return index < _bits.Length && _bits[index];
    }
}

internal static class ComponentTypeIndex<T>
{
    public static readonly int Index;
    static ComponentTypeIndex() => Index = typeof(T).GetHashCode() % 64;
}
