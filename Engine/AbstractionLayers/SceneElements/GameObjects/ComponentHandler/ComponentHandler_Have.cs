using Engine.SceneElements.Components.Characteristics;
using Engine.SceneElements.Components;
using System.Linq;
using System;

namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler
{
    // csharpier-ignore
    public bool HaveComponent<ComponentType>()
        where ComponentType : Component
    => _components.Any(c => c is ComponentType);

    // csharpier-ignore
    public bool HaveComponent(Type type) =>
        _components.Any(c => type.IsAssignableFrom(c.GetType()));

    private static bool HaveOnlyUniqueTypes(Component[] components)
    {
        // csharpier-ignore
        return components
            .Where(c => c is not IRepeatable)
            .Select(c => c.GetType())
            .Distinct()
            .Count() == components.Length;
    }

    // csharpier-ignore
    public bool HaveChild<T>() =>
        _children.Any(c => c is T);
}
