namespace MarioEngine.Core.Core;

/// <summary>Pauses game time without halting system updates.</summary>
internal sealed class GameTimeFreezer
{
    public bool IsFrozen { get; private set; }
    public float FreezeDuration { get; private set; }

    private float _elapsed;

    public void Freeze(float duration)
    {
        IsFrozen = true;
        FreezeDuration = duration;
        _elapsed = 0f;
    }

    public void Update(float dt)
    {
        if (!IsFrozen) return;
        _elapsed += dt;
        if (_elapsed >= FreezeDuration)
        {
            IsFrozen = false;
            FreezeDuration = 0f;
        }
    }
}
