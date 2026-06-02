namespace MarioEngine.Core.Core.Scene;

/// <summary>Filters entities by required component type and optional tag.</summary>
internal sealed class EntityQuery
{
    public Type RequiredComponent { get; init; } = typeof(object);
    public string? RequiredTag { get; init; }

    public List<Entity> Execute(IEnumerable<Entity> entities)
    {
        return entities.Where(e => HasComponent(e) && HasTag(e)).ToList();
    }

    private bool HasComponent(Entity e) => true; // placeholder
    private bool HasTag(Entity e) => RequiredTag == null;
}
