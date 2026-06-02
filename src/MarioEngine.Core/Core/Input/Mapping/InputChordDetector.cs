namespace MarioEngine.Core.Core.Input.Mapping;

/// <summary>Detects simultaneous key press chords.</summary>
internal sealed class InputChordDetector
{
    public List<Key> ChordKeys { get; } = new();
    public float ChordTimeout { get; set; } = 0.3f;

    public bool IsChordPressed(KeyboardDevice kb)
    {
        if (ChordKeys.Count == 0) return false;
        foreach (var key in ChordKeys)
            if (!kb.IsKeyDown(key)) return false;
        return true;
    }
}

internal enum Key { Unknown, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, Space, Enter, Escape, Up, Down, Left, Right }

internal sealed class KeyboardDevice
{
    public bool IsKeyDown(Key key) => false;
}
