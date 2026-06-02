namespace MarioEngine.Core.Input.Mapping;

internal sealed class ActionAxisMapping
{
    public string PositiveAction { get; }
    public string NegativeAction { get; }
    public float Value { get; private set; }

    public ActionAxisMapping(string positiveAction, string negativeAction)
    {
        PositiveAction = positiveAction;
        NegativeAction = negativeAction;
    }

    public void Update(bool positiveDown, bool negativeDown)
    {
        float pos = positiveDown ? 1f : 0f;
        float neg = negativeDown ? 1f : 0f;
        Value = pos - neg;
    }
}
