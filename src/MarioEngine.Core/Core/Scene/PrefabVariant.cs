namespace MarioEngine.Core.Core.Scene;

/// <summary>Inherits from a prefab with overridden properties.</summary>
internal sealed class PrefabVariant
{
    public Prefab BasePrefab { get; init; } = new();
    public Dictionary<string, object> Overrides { get; init; } = new();

    public Entity Instantiate()
    {
        var entity = BasePrefab.Instantiate();
        // Apply overrides to entity components
        return entity;
    }
}

internal sealed class Prefab
{
    public Entity Instantiate() => new();
}
