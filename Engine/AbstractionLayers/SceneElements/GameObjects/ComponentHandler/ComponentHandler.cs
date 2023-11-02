using Engine.SceneElements.Components;
using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler
{
    private readonly List<Component> _components = new();
    private readonly List<ComponentHandler> _children = new();

    //public ComponentHandler()
    //    : this(Array.Empty<Component>(), Array.Empty<ComponentHandler>()) { }

    public ComponentHandler(params Component[] components)
        : this(components, Array.Empty<ComponentHandler>()) { }

    public ComponentHandler(IEnumerable<Component> components)
        : this(components, Array.Empty<ComponentHandler>()) { }

    public ComponentHandler(IEnumerable<ComponentHandler> children)
        : this(Array.Empty<Component>(), children) { }

    public ComponentHandler(params ComponentHandler[] children)
        : this(Array.Empty<Component>(), children) { }

    public ComponentHandler(
        IEnumerable<Component> components,
        IEnumerable<ComponentHandler> children
    )
    {
        // csharpier-ignore
        Debug.Assert(
            components != null,
            $"parametr {nameof(components)} should not be null"
        );
        // csharpier-ignore
        Debug.Assert(
            children != null,
            $"parametr {nameof(children)} should not be null"
        );

        if (components.Count() != 0)
            AddComponents(components);

        if (children.Count() != 0)
            AddChildren(children);
    }
}
