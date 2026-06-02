namespace MarioEngine.Core.Physics.Zones;

/// <summary>
/// A trigger zone that executes an action when entered/exited.
/// </summary>
internal sealed class TriggerZone
{
    internal string Id { get; }
    internal bool OneShot { get; }
    private bool _fired;

    internal TriggerZone(string id, bool oneShot = false) { Id = id; OneShot = oneShot; }

    internal bool TryTrigger() { if (OneShot && _fired) return false; _fired = true; return true; }
    internal void Reset() => _fired = false;
}
