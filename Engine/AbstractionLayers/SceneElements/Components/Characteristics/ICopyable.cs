namespace Engine.SceneElements.Components.Characteristics;

public interface ICopyable<T>
{
    public abstract void CopyFrom(T from);
}
