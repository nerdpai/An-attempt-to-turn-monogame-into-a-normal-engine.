namespace Engine.SceneElements;

public interface ISingleOwner
{
    public abstract bool HasOwner { get; }

    public abstract void Engage();

    public abstract void Free();
}
