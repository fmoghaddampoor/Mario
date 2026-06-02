namespace MarioEngine.Core.Scene;

/// <summary>
/// A reusable blueprint that can instantiate new <see cref="Entity"/> instances on demand.
/// </summary>
internal sealed class Prefab
{
    /// <summary>Name used to identify this prefab.</summary>
    public string Name { get; }

    /// <summary>Function that creates a new <see cref="Entity"/> each time it is called.</summary>
    public Func<Entity> InstantiateFunc { get; }

    /// <summary>
    /// Creates a prefab with the given <paramref name="name"/> and <paramref name="instantiateFunc"/>.
    /// </summary>
    public Prefab(string name, Func<Entity> instantiateFunc)
    {
        Name = name;
        InstantiateFunc = instantiateFunc;
    }

    /// <summary>
    /// Creates a fresh <see cref="Entity"/> from the blueprint.
    /// </summary>
    public Entity Instantiate()
    {
        return InstantiateFunc();
    }

    /// <summary>
    /// Captures the structure of an existing <paramref name="source"/> entity
    /// and returns a <see cref="Prefab"/> that reproduces it.
    /// </summary>
    public static Prefab FromEntity(Entity source)
    {
        var components = source.GetAllComponents().ToList();

        return new Prefab(source.Tag, () =>
        {
            var clone = new Entity { Tag = source.Tag };

            foreach (var comp in components)
            {
                clone.AddComponent(comp);
            }

            return clone;
        });
    }
}
