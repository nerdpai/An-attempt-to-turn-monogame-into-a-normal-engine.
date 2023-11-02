namespace Engine.SceneElements.Characteristics;

public interface IUpdatable
{
    public abstract bool IsActive { get; }
    public abstract void Update(XNA::GameTime gameTime);
}
