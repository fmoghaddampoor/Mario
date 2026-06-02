namespace MarioEngine.Core.Input.Devices;

using System.Numerics;

internal static class DeadZoneProcessor
{
    public static float Apply(float value, float deadZone = 0.15f)
    {
        if (Math.Abs(value) < deadZone)
            return 0f;
        return Math.Sign(value) * ((Math.Abs(value) - deadZone) / (1f - deadZone));
    }

    public static Vector2 Apply(Vector2 value, float deadZone = 0.15f)
    {
        float magnitude = value.Length();
        if (magnitude < deadZone)
            return Vector2.Zero;
        Vector2 normalized = value / magnitude;
        float scaled = (magnitude - deadZone) / (1f - deadZone);
        return normalized * scaled;
    }
}
