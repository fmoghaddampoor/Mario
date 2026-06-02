using System.Numerics;

namespace MarioEngine.Core.Scene;

/// <summary>
/// Represents a game object in the scene. Owns a collection of <see cref="IComponent"/> instances.
/// </summary>
public partial class Entity
{
    private readonly List<IComponent> _components = new();

    /// <summary>Unique identifier assigned at creation.</summary>
    public Guid Id { get; }

    /// <summary>Arbitrary label used for grouping and lookup.</summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>Whether the entity is active and should be updated.</summary>
    public bool Active { get; set; } = true;

    /// <summary>True after <see cref="Destroy"/> has been called.</summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// Creates a new entity with a new <see cref="Guid"/>.
    /// </summary>
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Adds a component to this entity and calls <see cref="IComponent.Initialize"/>.
    /// </summary>
    public T? AddComponent<T>(T component) where T : IComponent
    {
        component.Initialize(this);
        _components.Add(component);
        return component;
    }

    /// <summary>
    /// Removes the first component of type <typeparamref name="T"/> and calls <see cref="IComponent.Destroy"/>.
    /// </summary>
    public void RemoveComponent<T>() where T : IComponent
    {
        var comp = _components.OfType<T>().FirstOrDefault();
        if (comp is not null)
        {
            comp.Destroy();
            _components.Remove(comp);
        }
    }

    /// <summary>
    /// Returns the first component of type <typeparamref name="T"/>, or <c>null</c>.
    /// </summary>
    public T? GetComponent<T>() where T : IComponent
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Returns all components attached to this entity.
    /// </summary>
    public IEnumerable<IComponent> GetAllComponents()
    {
        return _components.AsReadOnly();
    }

    /// <summary>
    /// Updates every active component with the given delta time.
    /// </summary>
    public void Update(float dt)
    {
        if (!Active) return;

        foreach (var component in _components)
        {
            component.Update(dt);
        }
    }

    /// <summary>
    /// Marks the entity as destroyed and calls <see cref="IComponent.Destroy"/> on every component.
    /// </summary>
    public void Destroy()
    {
        if (IsDestroyed) return;

        IsDestroyed = true;

        foreach (var component in _components)
        {
            component.Destroy();
        }

        _components.Clear();
    }
}
