using Engine.SceneElements.Components;
using System.Diagnostics;
using System.Linq;
using Engine.SceneElements.Components.Characteristics;
using System;
using System.Collections.Generic;

namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler
{
    private readonly Dictionary<Destroyable, Action> _removingPromise = new();

    public ComponentHandler AddChildren(params ComponentHandler[] children) =>
        AddChildren((IEnumerable<ComponentHandler>)children);

    public ComponentHandler AddChildren(IEnumerable<ComponentHandler> children)
    {
        Debug.Assert(
            children != null && children.Count() != 0,
            $"parametr {nameof(children)} should not be empty"
        );
        Debug.Assert(
            !children.Any(c => c == null),
            $"parametr {nameof(children)} should not be contain null"
        );

        foreach (var child in children)
        {
            child.Engage();
            void remove() => RemoveChild(child);
            child.OnDestroy += remove;
            _removingPromise[child] = remove;
        }

        Binding(children);
        _children.AddRange(children);

        return this;
    }

    public ComponentHandler RemoveChild(ComponentHandler child)
    {
        // csharpier-ignore
        Debug.Assert(
            child != null,
            $"parametr {nameof(child)} should not be null"
        );

        child.Free();
        child.OnDestroy -= _removingPromise[child];
        _removingPromise.Remove(child);

        Unbinding(child);
        _children.Remove(child);

        return this;
    }

    public ComponentHandler AddComponents(params Component[] components) =>
        AddComponents((IEnumerable<Component>)components);

    public ComponentHandler AddComponents(IEnumerable<Component> components)
    {
        Debug.Assert(
            components != null && components.Count() != 0,
            $"parametr {nameof(components)} should not be empty"
        );
        Debug.Assert(
            !components.Any(c => c == null),
            $"parametr {nameof(components)} should not be contain null"
        );
        // csharpier-ignore
        Debug.Assert(
            HaveOnlyUniqueTypes(components.ToArray())
                && components
                    .Where(c => c is not IRepeatable)
                    .Select(c => c.GetType())
                    .Intersect(

                        _components
                                .Where(c => c is not IRepeatable)
                                .Select(c => c.GetType())
                    )
                    .Count() == 0,
            "Components shouldn't be repeaten"
        );

        foreach (var component in components)
        {
            component.Engage();
            void remove() => RemoveComponent(component);
            component.OnDestroy += remove;
            _removingPromise[component] = remove;
        }

        Binding(components);
        _components.AddRange(components);
        return this;
    }

    public ComponentHandler RemoveComponent(Component component)
    {
        // csharpier-ignore
        Debug.Assert(
            component != null,
            $"parametr {nameof(component)} should not be null"
        );

        component.Free();
        component.OnDestroy -= _removingPromise[component];
        _removingPromise.Remove(component);

        Unbinding(component);
        _components.Remove(component);
        return this;
    }
}
