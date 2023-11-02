namespace Engine.SceneElements.Animations.Curves;

public abstract class Curve : ParametricCurve<float>
{
    public virtual Curve Flip() => new FlippedCurve(this);
}
