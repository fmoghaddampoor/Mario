namespace MarioEngine.Core.Core.Physics;

/// <summary>Defines which physics layers collide with each other.</summary>
internal static class CollisionMatrix
{
    public static bool[,] Matrix { get; } = new bool[16, 16];

    static CollisionMatrix()
    {
        for (int i = 0; i < 16; i++)
            for (int j = 0; j < 16; j++)
                Matrix[i, j] = true;
    }

    public static bool ShouldCollide(int layerA, int layerB)
    {
        if (layerA < 0 || layerA >= 16 || layerB < 0 || layerB >= 16) return false;
        return Matrix[layerA, layerB];
    }
}
