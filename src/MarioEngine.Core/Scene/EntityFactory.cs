using System.Numerics;

namespace MarioEngine.Core.Scene;

/// <summary>
/// Static factory that creates entities from registered blueprints or built-in templates.
/// </summary>
internal static class EntityFactory
{
    private static readonly Dictionary<string, Func<Entity>> _blueprints = new();

    /// <summary>
    /// Registers a blueprint under <paramref name="name"/>.
    /// </summary>
    public static void Register(string name, Func<Entity> factory)
    {
        _blueprints[name] = factory;
    }

    /// <summary>
    /// Creates an entity from a previously registered blueprint.
    /// </summary>
    public static Entity Create(string name)
    {
        if (_blueprints.TryGetValue(name, out var factory))
        {
            return factory();
        }

        throw new KeyNotFoundException($"No blueprint registered with name '{name}'.");
    }

    /// <summary>
    /// Creates a bare entity with no components.
    /// </summary>
    public static Entity CreateEmpty()
    {
        return new Entity();
    }

    /// <summary>
    /// Creates an entity with a <see cref="Components.TransformComponent"/> at the given position.
    /// </summary>
    public static Entity CreateWithTransform(Vector2 position)
    {
        var entity = new Entity();
        var transform = new Components.TransformComponent { Position = position };
        entity.AddComponent(transform);
        return entity;
    }
}
