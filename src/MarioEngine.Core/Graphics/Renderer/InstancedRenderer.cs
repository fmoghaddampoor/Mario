namespace MarioEngine.Core.Graphics.Renderer;

/// <summary>Renders many identical objects in a single draw call.</summary>
internal sealed class InstancedRenderer
{
    public int InstanceCount { get; private set; }

    private readonly List<Matrix4x4> _instances = new();

    public void Begin()
    {
        _instances.Clear();
        InstanceCount = 0;
    }

    public void AddInstance(Matrix4x4 transform)
    {
        _instances.Add(transform);
        InstanceCount = _instances.Count;
    }

    public void End()
    {
        // Bind instance buffer and call glDrawElementsInstanced
    }
}

internal readonly struct Matrix4x4
{
    // Placeholder for actual matrix type
}
