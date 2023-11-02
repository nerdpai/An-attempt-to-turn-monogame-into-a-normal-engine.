using Engine.SceneElements.Components;
using System.Linq;
using System;

namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler
{
    public ComponentType GetComponent<ComponentType>()
        where ComponentType : Component
    {
        // csharpier-ignore
        return _components
            .OfType<ComponentType>()
            .DefaultIfEmpty()
            .ElementAt(0);
    }

    public Component GetComponent(Type type)
    {
        // csharpier-ignore
        return _components
            .Where(c => type.IsAssignableFrom(c.GetType()))
            .DefaultIfEmpty()
            .ElementAt(0);
    }

    public ComponentType[] GetComponents<ComponentType>()
        where ComponentType : Component
    {
        // csharpier-ignore
        return _components
            .OfType<ComponentType>()
            .ToArray();
    }

    public Component[] GetComponents(Type type)
    {
        // csharpier-ignore
        return _components
            .Where(c => type.IsAssignableFrom(c.GetType()))
            .ToArray();
    }

    public T[] GetChildren<T>()
    {
        // csharpier-ignore
        return _children
        .OfType<T>()
        .ToArray();
    }
}
