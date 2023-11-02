using Engine.SceneElements.Components;
using System.Diagnostics;
using System.Linq;
using Engine.SceneElements.Components.Characteristics;
using System;
using System.Collections.Generic;

namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler
{
    private void AllChildrenShouldHave(Type type)
    {
        Debug.Assert(
            _children.All(c => c.HaveComponent(type)),
            $"Not all children have {type.Name} or parent class that have their DADDY"
        );
    }

    private void Binding(IEnumerable<Component> components)
    {
        var bindables = GetAllBindableComponents(components);

        foreach (var component in bindables)
        {
            AllChildrenShouldHave(component.BindType);

            foreach (var child in _children)
                component.Bind(child.GetComponent(component.BindType));
        }
    }

    private void Unbinding(Component component)
    {
        IBindable bindable = (IBindable)component;
        foreach (var child in _children)
            bindable.Unbind(child.GetComponent(bindable.BindType));
    }

    private void Binding(IEnumerable<ComponentHandler> children)
    {
        IBindable[] bindables = GetAllBindableComponents(_components);
        foreach (var component in bindables)
        {
            foreach (var child in children)
                component.Bind(child.GetComponent(component.BindType));
        }
    }

    private void Unbinding(ComponentHandler child)
    {
        IBindable[] bindables = GetAllBindableComponents(_components);
        foreach (var component in bindables)
            component.Unbind(child.GetComponent(component.BindType));
    }

    private static IBindable[] GetAllBindableComponents(IEnumerable<Component> components)
    {
        // csharpier-ignore
        return components
        .OfType<IBindable>()
        .ToArray();
    }
}
