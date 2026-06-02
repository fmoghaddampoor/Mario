namespace MarioEngine.Core.Core.Scene;

/// <summary>Hierarchical parent-child entity relationship graph.</summary>
internal sealed class EntityRelationshipGraph
{
    private readonly Dictionary<Guid, List<Guid>> _parentChild = new();

    public void AddRelation(Guid parent, Guid child)
    {
        if (!_parentChild.ContainsKey(parent)) _parentChild[parent] = new();
        _parentChild[parent].Add(child);
    }

    public List<Guid> GetChildren(Guid id)
    {
        return _parentChild.TryGetValue(id, out var children) ? children : new();
    }
}
