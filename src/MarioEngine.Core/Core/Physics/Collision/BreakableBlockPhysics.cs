namespace MarioEngine.Core.Core.Physics.Collision;

/// <summary>Spawns debris particles when a block breaks.</summary>
internal static class BreakableBlockPhysics
{
    public static void SpawnDebris(Vector2 position, Vector2 velocity, int pieceCount)
    {
        for (int i = 0; i < pieceCount; i++)
        {
            float angle = (float)(i * Math.PI * 2 / pieceCount);
            var dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            // Spawn debris entity at position with dir * velocity
        }
    }
}
