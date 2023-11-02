using System.Diagnostics;

namespace Engine.SceneElements.Animations.Curves;

public abstract class ParametricCurve<ReturnType>
{
    protected abstract ReturnType EvaluateValueAtTime(double time);

    public ReturnType GetValueAtTime(double time)
    {
        Debug.Assert(
            time is >= 0.0 and <= 1.0,
            $"parametr {nameof(time)} should be in [0, 1] range."
        );
        return EvaluateValueAtTime(time);
    }
}
