using Engine.SceneElements.Components.Characteristics;
using System;

namespace Engine.SceneElements.Components;

public abstract class BindableComponent<T> : Component, IBindable<T>
    where T : BindableComponent<T>
{
    public Type BindType
    {
        get => typeof(T);
    }

    public abstract void Bind(T bindItToMyself);

    public void Bind(Component component) => Bind((T)component);

    public abstract void Unbind(T unbindItFromMyself);

    public void Unbind(Component component) => Unbind((T)component);
}
