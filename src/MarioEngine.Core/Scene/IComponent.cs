namespace MarioEngine.Core.Scene;

/// <summary>
/// Defines the lifecycle interface for all components attached to an Entity.
/// </summary>
public interface IComponent
{
    /// <summary>
    /// Called once when the component is added to an entity.
    /// </summary>
    void Initialize(Entity owner);

    /// <summary>
    /// Called every frame with the delta time in seconds.
    /// </summary>
    void Update(float dt);

    /// <summary>
    /// Called when the component (or its owning entity) is destroyed.
    /// </summary>
    void Destroy();
}
