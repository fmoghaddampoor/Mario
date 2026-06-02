namespace MarioEngine.Core.Physics;

/// <summary>
/// Physics material properties defining surface behavior.
/// Applied to colliders to control friction and bounciness.
/// </summary>
internal sealed class PhysicsMaterial
{
    /// <summary>Default physics material (moderate friction, no bounce).</summary>
    internal static readonly PhysicsMaterial Default = new PhysicsMaterial(0.5f, 0.3f, 0f);

    /// <summary>Ice material (very low friction).</summary>
    internal static readonly PhysicsMaterial Ice = new PhysicsMaterial(0.05f, 0.02f, 0.1f);

    /// <summary>Rubber material (high bounce).</summary>
    internal static readonly PhysicsMaterial Rubber = new PhysicsMaterial(0.6f, 0.4f, 0.8f);

    /// <summary>Initializes a new instance of the <see cref="PhysicsMaterial"/> class.</summary>
    /// <param name="staticFriction">Static friction coefficient.</param>
    /// <param name="dynamicFriction">Dynamic friction coefficient.</param>
    /// <param name="bounciness">Bounciness (0.0 to 1.0).</param>
    public PhysicsMaterial(float staticFriction, float dynamicFriction, float bounciness)
    {
        StaticFriction = staticFriction;
        DynamicFriction = dynamicFriction;
        Bounciness = Math.Clamp(bounciness, 0f, 1f);
    }

    /// <summary>Gets the static friction coefficient (0.0 = ice, 1.0 = sandpaper).</summary>
    internal float StaticFriction { get; }

    /// <summary>Gets the dynamic friction coefficient.</summary>
    internal float DynamicFriction { get; }

    /// <summary>Gets the bounciness (0.0 = no bounce, 1.0 = perfect bounce).</summary>
    internal float Bounciness { get; }
}
