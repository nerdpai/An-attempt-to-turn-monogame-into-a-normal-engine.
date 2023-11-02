using System;

namespace Engine.SceneElements.Components.Characteristics;

public interface IBindable
{
    public abstract Type BindType { get; }
    public abstract void Bind(Component component);
    public abstract void Unbind(Component component);
}

public interface IBindable<in ComponentType> : IBindable
    where ComponentType : Component
{
    public abstract void Bind(ComponentType bindItToMyself);
    public abstract void Unbind(ComponentType unbindItFromMyself);
}
