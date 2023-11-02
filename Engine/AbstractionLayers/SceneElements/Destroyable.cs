using System;

namespace Engine.SceneElements;

public abstract class Destroyable
{
    public bool IsAlife { get; private set; } = true;
    public event Action OnDestroy;

    public virtual void Destroy()
    {
        IsAlife = false;
        OnDestroy?.Invoke();
    }
}
