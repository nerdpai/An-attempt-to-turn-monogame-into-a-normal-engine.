namespace Engine.SceneElements.Animations.Curves;

class FlippedCurve : Curve
{
    private readonly Curve _curve;

    public FlippedCurve(Curve curve) => _curve = curve;

    protected override float EvaluateValueAtTime(double time) =>
        1.0f - _curve.GetValueAtTime(1.0 - time);

    public override Curve Flip() => _curve;
}
