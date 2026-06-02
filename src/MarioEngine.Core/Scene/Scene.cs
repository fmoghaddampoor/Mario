namespace MarioEngine.Core.Scene;

/// <summary>
/// A collection of entities that are updated together.
/// Supports deferred add / remove to avoid mutation during iteration.
/// </summary>
internal sealed class Scene
{
    private readonly List<Entity> _entities = new();
    private readonly List<Entity> _pendingAdd = new();
    private readonly List<Entity> _pendingRemove = new();

    /// <summary>Human-readable name of this scene.</summary>
    public string Name { get; }

    /// <summary>
    /// Creates a new scene with the given <paramref name="name"/>.
    /// </summary>
    public Scene(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Finds an entity by its <see cref="Guid"/>.
    /// </summary>
    public Entity? FindEntity(Guid id)
    {
        return _entities.Find(e => e.Id == id);
    }

    /// <summary>
    /// Returns all entities whose <see cref="Entity.Tag"/> matches <paramref name="tag"/>.
    /// </summary>
    public IEnumerable<Entity> FindEntities(string tag)
    {
        return _entities.Where(e => e.Tag == tag);
    }

    /// <summary>
    /// Creates a new entity with the given <paramref name="tag"/> and adds it to the scene.
    /// </summary>
    public Entity CreateEntity(string tag = "default")
    {
        var entity = new Entity { Tag = tag };
        _pendingAdd.Add(entity);
        return entity;
    }

    /// <summary>
    /// Marks <paramref name="entity"/> for removal on the next update.
    /// </summary>
    public void DestroyEntity(Entity entity)
    {
        if (!_pendingRemove.Contains(entity) && _entities.Contains(entity))
        {
            _pendingRemove.Add(entity);
        }
    }

    /// <summary>
    /// Processes pending additions and removals, then updates all active entities.
    /// </summary>
    public void Update(float dt)
    {
        ProcessPending();

        foreach (var entity in _entities)
        {
            entity.Update(dt);
        }
    }

    /// <summary>
    /// Destroys every entity and clears the scene.
    /// </summary>
    public void Clear()
    {
        foreach (var entity in _entities)
        {
            entity.Destroy();
        }

        _entities.Clear();
        _pendingAdd.Clear();
        _pendingRemove.Clear();
    }

    private void ProcessPending()
    {
        if (_pendingRemove.Count > 0)
        {
            foreach (var entity in _pendingRemove)
            {
                entity.Destroy();
                _entities.Remove(entity);
            }

            _pendingRemove.Clear();
        }

        if (_pendingAdd.Count > 0)
        {
            _entities.AddRange(_pendingAdd);
            _pendingAdd.Clear();
        }
    }
}
