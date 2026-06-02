namespace MarioEngine.Core.Graphics.Buffer;

/// <summary>Reusable FBO pool with LRU eviction.</summary>
internal sealed class RenderTargetPool
{
    private readonly Dictionary<ulong, uint> _pool = new();
    private readonly Queue<ulong> _lru = new();
    private int _maxSize = 16;

    public uint GetFBO(int width, int height)
    {
        var key = PackKey(width, height);
        if (_pool.TryGetValue(key, out var fbo))
            return fbo;

        if (_pool.Count >= _maxSize)
            EvictOne();

        var newFbo = CreateFBO(width, height);
        _pool[key] = newFbo;
        return newFbo;
    }

    public void ReturnFBO(uint fbo)
    {
        var kv = _pool.FirstOrDefault(x => x.Value == fbo);
        if (kv.Key != 0)
            _lru.Enqueue(kv.Key);
    }

    private void EvictOne()
    {
        if (_lru.Count == 0) return;
        var key = _lru.Dequeue();
        _pool.Remove(key);
    }

    private static ulong PackKey(int w, int h) =>
        ((ulong)(ushort)w << 16) | (ushort)h;

    private static uint CreateFBO(int w, int h) => 1;
}
