using System;

namespace Engine.SceneElements.Components;

public abstract class Component : Destroyable, ISingleOwner
{
    public bool HasOwner { get; private set; } //= false;

    public void Engage()
    {
        if (HasOwner)
            throw new InvalidOperationException("The component is already used");
        HasOwner = true;
    }

    public void Free()
    {
        if (!HasOwner)
            throw new InvalidOperationException("The component is already used");
        HasOwner = false;
    }
}
