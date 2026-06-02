namespace MarioEngine.Core.Input.Devices;

using System.Collections.Generic;
using Silk.NET.Input;

/// <summary>
/// Wraps a Silk.NET IKeyboard and exposes key state per frame.
/// </summary>
internal sealed class KeyboardDevice : InputDevice
{
    private IKeyboard? _keyboard;
    private readonly HashSet<Key> _previous = new();
    private readonly HashSet<Key> _pressed = new();
    private readonly HashSet<Key> _justPressed = new();
    private readonly HashSet<Key> _justReleased = new();

    public KeyboardDevice() { Name = "Keyboard"; Type = DeviceType.Keyboard; }

    public void Bind(IKeyboard kb) => _keyboard = kb;

    public override bool IsConnected => _keyboard?.IsConnected ?? false;

    public override void Update()
    {
        _justPressed.Clear();
        _justReleased.Clear();
        foreach (var key in _pressed) _previous.Add(key);
        _pressed.Clear();

        if (_keyboard is null) return;

        foreach (var key in _previous)
        {
            if (_keyboard.IsKeyPressed(key)) _pressed.Add(key);
            else _justReleased.Add(key);
        }
    }

    internal bool IsKeyDown(Key key) => _pressed.Contains(key);
    internal bool IsKeyJustPressed(Key key) => _justPressed.Contains(key);
    internal bool IsKeyJustReleased(Key key) => _justReleased.Contains(key);
}
