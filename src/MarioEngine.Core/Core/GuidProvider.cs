namespace MarioEngine.Core.Core;

/// <summary>Provides GUIDs with optional deterministic mode for testing.</summary>
internal sealed class GuidProvider
{
    public bool Deterministic { get; set; }
    private int _counter;

    public Guid NewGuid()
    {
        if (Deterministic)
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(_counter++).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
        return Guid.NewGuid();
    }
}
