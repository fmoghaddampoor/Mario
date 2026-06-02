namespace MarioEngine.Core.Input;

using Silk.NET.Input;

internal sealed class InputAction
{
    public string Name { get; }
    public List<Key> Keys { get; }
    public float Value { get; private set; }
    public bool Pressed { get; private set; }
    public bool JustPressed { get; private set; }
    public bool JustReleased { get; private set; }

    private bool _previousPressed;

    public InputAction(string name, params Key[] keys)
    {
        Name = name;
        Keys = new List<Key>(keys);
    }

    public void Update(bool isDown, float analogValue = 0f)
    {
        Value = Math.Clamp(analogValue, 0f, 1f);
        JustPressed = isDown && !_previousPressed;
        JustReleased = !isDown && _previousPressed;
        Pressed = isDown;
        _previousPressed = isDown;
    }
}
