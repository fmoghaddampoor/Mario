namespace MarioEngine.Core.Core.Scene;

/// <summary>Full scene state capture and restore.</summary>
internal sealed class SceneSnapshot
{
    public byte[] SerializedData { get; init; } = Array.Empty<byte>();

    public static SceneSnapshot Capture(Scene scene)
    {
        var data = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(scene);
        return new SceneSnapshot { SerializedData = data };
    }

    public static Scene Restore(SceneSnapshot snapshot)
    {
        return System.Text.Json.JsonSerializer.Deserialize<Scene>(snapshot.SerializedData) ?? new Scene();
    }
}

internal sealed class Scene { }
