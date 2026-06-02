namespace MarioEngine.Core.Core;

/// <summary>Queues actions for execution on the main thread.</summary>
internal sealed class ThreadDispatcher
{
    private readonly Queue<Action> _pending = new();
    private readonly object _lock = new();

    public void ExecuteOnMainThread(Action action)
    {
        lock (_lock) { _pending.Enqueue(action); }
    }

    public void Update()
    {
        Queue<Action> snapshot;
        lock (_lock)
        {
            snapshot = new Queue<Action>(_pending);
            _pending.Clear();
        }
        while (snapshot.Count > 0) snapshot.Dequeue()?.Invoke();
    }
}
