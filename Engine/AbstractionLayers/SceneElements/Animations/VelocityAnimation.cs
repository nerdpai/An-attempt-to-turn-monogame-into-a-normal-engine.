using System;
using System.Diagnostics;
using System.Numerics;

namespace Engine.SceneElements.Animations;

public class VelocityAnimation<T> : ParametricAnimation<T>
    where T : IAdditionOperators<T, T, T>,
        IMultiplyOperators<T, float, T>,
        ISubtractionOperators<T, T, T>
{
    private float _speedInSecond;

    public VelocityAnimation(float speedInSecond)
    {
        Debug.Assert(
            speedInSecond > 0,
            $"parametr {nameof(speedInSecond)} should be in [0, infinity)  range."
        );
        _speedInSecond = speedInSecond;
    }

    public override ParametricAnimation<T1> ForParametr<T1>() =>
        new VelocityAnimation<T1>(_speedInSecond);

    protected override void EveryUpdate(XNA::GameTime gameTime) =>
        throw new NotImplementedException();
}
