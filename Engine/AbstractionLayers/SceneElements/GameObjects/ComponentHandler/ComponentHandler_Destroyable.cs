namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler : Destroyable
{
    public override void Destroy()
    {
        foreach (var child in _children)
            child.Destroy();
        foreach (var component in _components)
            component.Destroy();

        base.Destroy();
    }
}
