using System;
using System.Numerics;
using Engine.SceneElements.Characteristics;

namespace Engine.SceneElements.Animations;

public abstract class ParametricAnimation<T> : Animation, IUpdatable
    where T : IAdditionOperators<T, T, T>,
        IMultiplyOperators<T, float, T>,
        ISubtractionOperators<T, T, T>
{
    protected T _start;
    protected T _end;
    protected T _diffrence;

    public T Value { get; protected set; }

    public bool IsActive { get; private set; } = true;

    public bool IsComplited { get; protected set; } = true;

    public virtual void Start(T start, T end)
    {
        _start = start;
        _end = end;
        _diffrence = end - start;
        Value = start;
        IsComplited = false;
        IsActive = true;
    }

    public void Stop() => IsActive = false;

    public void Continue() => IsActive = true;

    protected Action<T> _onUpdate;

    /// <summary>
    /// Event that invoke all delegates with animation <c>Value</c> argument
    /// </summary>
    /// <remarks>
    /// It's stop working when animation <c>IsComplited</c> or <c>IsActive</c> is false
    /// </remarks>
    public event Action<T> OnUpdate
    {
        add => _onUpdate += value;
        remove => _onUpdate -= value;
    }

    public void Update(XNA::GameTime gameTime)
    {
        if (!IsComplited && IsActive)
        {
            EveryUpdate(gameTime);
        }
    }

    protected abstract void EveryUpdate(XNA::GameTime gameTime);
}
