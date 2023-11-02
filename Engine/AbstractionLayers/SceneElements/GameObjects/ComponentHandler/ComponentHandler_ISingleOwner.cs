using System;

namespace Engine.SceneElements.GameObjects;

public abstract partial class ComponentHandler : ISingleOwner
{
    public bool HasOwner { get; private set; } //= false;

    public void Engage()
    {
        if (HasOwner)
            throw new InvalidOperationException("The gameObject is already used");
        HasOwner = true;
    }

    public void Free()
    {
        if (!HasOwner)
            throw new InvalidOperationException("The gameObject is already used");
        HasOwner = false;
    }
}
