using System.Numerics;

namespace Engine.SceneElements.Animations;

public abstract class Animation
{
    public abstract ParametricAnimation<T> ForParametr<T>()
        where T : IAdditionOperators<T, T, T>,
            IMultiplyOperators<T, float, T>,
            ISubtractionOperators<T, T, T>;
}
