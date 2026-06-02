namespace MarioEngine.Core.Input.Recording;

using Silk.NET.Input;
using System.Text.Json;

internal sealed class InputRecorder
{
    private readonly List<(float Time, Key Key, bool Pressed)> _recordedData = new();
    private float _currentTime;

    public IReadOnlyList<(float Time, Key Key, bool Pressed)> RecordedData => _recordedData.AsReadOnly();

    public void RecordFrame(float dt, Devices.KeyboardDevice keyboard)
    {
        _currentTime += dt;
        var keys = Enum.GetValues<Key>();
        foreach (var key in keys)
        {
            if (key == Key.Unknown) continue;
            if (keyboard.IsKeyJustPressed(key))
                _recordedData.Add((_currentTime, key, true));
            else if (keyboard.IsKeyJustReleased(key))
                _recordedData.Add((_currentTime, key, false));
        }
    }

    public void Save(string filePath)
    {
        var entries = _recordedData.Select(e => new RecordedEntry { Time = e.Time, Key = (int)e.Key, Pressed = e.Pressed }).ToList();
        var json = JsonSerializer.Serialize(entries);
        File.WriteAllText(filePath, json);
    }

    public void Load(string filePath)
    {
        if (!File.Exists(filePath)) return;
        var json = File.ReadAllText(filePath);
        var entries = JsonSerializer.Deserialize<List<RecordedEntry>>(json);
        if (entries is null) return;
        _recordedData.Clear();
        foreach (var e in entries)
            _recordedData.Add((e.Time, (Key)e.Key, e.Pressed));
    }

    public void Clear()
    {
        _recordedData.Clear();
        _currentTime = 0f;
    }

    private struct RecordedEntry
    {
        public float Time { get; set; }
        public int Key { get; set; }
        public bool Pressed { get; set; }
    }
}
