namespace MarioEngine.Core.Core.Scene;

/// <summary>Resolves component update ordering based on dependencies.</summary>
internal sealed class ComponentDependencyGraph
{
    private readonly Dictionary<Type, List<Type>> _dependencies = new();

    public void AddDependency(Type component, Type dependsOn)
    {
        if (!_dependencies.ContainsKey(component)) _dependencies[component] = new();
        _dependencies[component].Add(dependsOn);
    }

    public List<Type> GetUpdateOrder(List<Type> components)
    {
        var result = new List<Type>();
        var visited = new HashSet<Type>();
        foreach (var c in components) Visit(c, components, visited, result);
        return result;
    }

    private void Visit(Type type, List<Type> all, HashSet<Type> visited, List<Type> result)
    {
        if (visited.Contains(type) || !all.Contains(type)) return;
        visited.Add(type);
        if (_dependencies.TryGetValue(type, out var deps))
            foreach (var d in deps) Visit(d, all, visited, result);
        result.Add(type);
    }
}
