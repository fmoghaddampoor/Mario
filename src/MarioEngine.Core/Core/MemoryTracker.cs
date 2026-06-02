namespace MarioEngine.Core.Core;

/// <summary>Tracks managed memory and GC pressure.</summary>
internal static class MemoryTracker
{
    private static long _lastAllocated;
    private static int _collectionCount;

    public static long GetAllocatedMemory()
    {
        _lastAllocated = GC.GetTotalMemory(false);
        return _lastAllocated;
    }

    public static float GetGCPressure()
    {
        int current = GC.CollectionCount(0);
        float pressure = (float)(current - _collectionCount);
        _collectionCount = current;
        return pressure;
    }
}
