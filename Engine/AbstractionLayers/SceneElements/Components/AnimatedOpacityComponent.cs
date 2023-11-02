using Engine.SceneElements.Animations;

namespace Engine.SceneElements.Components;

public class AnimatedOpacityComponent : OpacityComponent
{
    public AnimatedOpacityComponent(Animation animation)
    {
        _opacityAnimation = animation.ForParametr<float>();
        _opacityAnimation.OnUpdate += (value) => base.Opacity = value;
    }

    private readonly ParametricAnimation<float> _opacityAnimation;
    public new float Opacity
    {
        get => base.Opacity;
        set => _opacityAnimation.Start(base.Opacity, value);
    }
}
