using Engine.SceneElements.Animations;
using Engine.SceneElements.Characteristics;
using Engine.SceneElements.Components.Other;

namespace Engine.SceneElements.Components;

public class AnimatedTransformComponent : TransformComponent, IUpdatable
{
    public AnimatedTransformComponent(Animation animation)
    {
        _centerAnimation = animation.ForParametr<Vector2>();
        _scaleAnimation = animation.ForParametr<Vector2>();
        _rotationAnimation = animation.ForParametr<Angle>();
        _positionAnimation = animation.ForParametr<Vector3>();
        _sizeAnimation = animation.ForParametr<Size>();

        _centerAnimation.OnUpdate += (value) => base.Center = value;
        _scaleAnimation.OnUpdate += (value) => base.Scale = value;
        _rotationAnimation.OnUpdate += (value) => base.Rotation = value;
        _positionAnimation.OnUpdate += (value) => base.Position = value;
        _sizeAnimation.OnUpdate += (value) => base.Size = value;
    }

    private readonly ParametricAnimation<Size> _sizeAnimation;
    public new Size Size
    {
        get => base.Size;
        set => _sizeAnimation.Start(base.Size, value);
    }

    private readonly ParametricAnimation<Vector3> _positionAnimation;
    public new Vector3 Position
    {
        get => base.Position;
        set => _positionAnimation.Start(base.Position, value);
    }

    private readonly ParametricAnimation<Angle> _rotationAnimation;
    public new Angle Rotation
    {
        get => base.Rotation;
        set => _rotationAnimation.Start(base.Rotation, value);
    }

    private readonly ParametricAnimation<Vector2> _scaleAnimation;
    public new Vector2 Scale
    {
        get => base.Scale;
        set => _scaleAnimation.Start(base.Scale, value);
    }

    private readonly ParametricAnimation<Vector2> _centerAnimation;
    public new Vector2 Center
    {
        get => base.Center;
        set => _centerAnimation.Start(base.Center, value);
    }

    public new void CopyFrom(TransformComponent from)
    {
        Position = from.Position;
        Rotation = from.Rotation;
        Scale = from.Scale;
        Size = from.Size;
    }

    public bool IsActive { get; private set; }

    public void Update(XNA.GameTime gameTime)
    {
        _centerAnimation.Update(gameTime);
        _positionAnimation.Update(gameTime);
        _rotationAnimation.Update(gameTime);
        _scaleAnimation.Update(gameTime);
        _sizeAnimation.Update(gameTime);
    }
}
