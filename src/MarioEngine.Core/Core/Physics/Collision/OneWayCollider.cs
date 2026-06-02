namespace MarioEngine.Core.Core.Physics.Collision;

/// <summary>Collider that only blocks from specific directions.</summary>
internal enum AllowedDirection { TopOnly, BottomOnly, LeftOnly, RightOnly }

internal sealed class OneWayCollider
{
    public AllowedDirection Direction { get; set; }

    public bool CanCollide(Vector2 approachDirection)
    {
        return Direction switch
        {
            AllowedDirection.TopOnly => approachDirection.Y < 0,
            AllowedDirection.BottomOnly => approachDirection.Y > 0,
            AllowedDirection.LeftOnly => approachDirection.X < 0,
            AllowedDirection.RightOnly => approachDirection.X > 0,
            _ => false
        };
    }
}
