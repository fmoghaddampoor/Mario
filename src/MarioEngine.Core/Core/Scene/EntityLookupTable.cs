namespace MarioEngine.Core.Core.Scene;

/// <summary>Fast O(1) entity lookup by GUID.</summary>
internal sealed class EntityLookupTable
{
    private readonly Dictionary<Guid, Entity> _entities = new();

    public void Add(Entity e) => _entities[e.Id] = e;
    public void Remove(Guid id) => _entities.Remove(id);
    public Entity? Find(Guid id) => _entities.TryGetValue(id, out var e) ? e : null;
}

internal sealed class Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
