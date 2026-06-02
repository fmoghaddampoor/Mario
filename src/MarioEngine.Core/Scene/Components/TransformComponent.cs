using System.Numerics;

namespace MarioEngine.Core.Scene.Components;

/// <summary>
/// Position, rotation and scale data for an entity. Supports parent-child hierarchies.
/// </summary>
internal sealed class TransformComponent : IComponent
{
    private Entity _owner = null!;
    private float _rotation;

    /// <summary>World-space position.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Local scale factor. Defaults to <see cref="Vector2.One"/>.</summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>World-space rotation in degrees.</summary>
    public float Rotation
    {
        get => _rotation;
        set => _rotation = value % 360f;
    }

    /// <summary>Position relative to the parent transform's position.</summary>
    public Vector2 LocalPosition
    {
        get => Parent is not null ? Position - Parent.Position : Position;
        set => Position = Parent is not null ? Parent.Position + value : value;
    }

    /// <summary>Optional parent transform. Affects local-space calculations.</summary>
    public TransformComponent? Parent { get; set; }

    /// <summary>Moves the entity by <paramref name="delta"/> in world space.</summary>
    public void Translate(Vector2 delta)
    {
        Position += delta;
    }

    /// <summary>Rotates by <paramref name="angleDegrees"/> degrees.</summary>
    public void Rotate(float angleDegrees)
    {
        Rotation += angleDegrees;
    }

    void IComponent.Initialize(Entity owner)
    {
        _owner = owner;
    }

    void IComponent.Update(float dt)
    {
    }

    void IComponent.Destroy()
    {
    }
}
