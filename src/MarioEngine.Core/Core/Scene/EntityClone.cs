namespace MarioEngine.Core.Core.Scene;

/// <summary>Deep clones an entity with all components.</summary>
internal static class EntityClone
{
    public static Entity DeepClone(Entity source)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(source);
        return System.Text.Json.JsonSerializer.Deserialize<Entity>(json) ?? new Entity();
    }
}
