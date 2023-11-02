using System;
using System.Numerics;
using Engine.SceneElements.Animations.Curves;

namespace Engine.SceneElements.Animations;

public class DurationAnimation<T> : ParametricAnimation<T>
    where T : IAdditionOperators<T, T, T>,
        IMultiplyOperators<T, float, T>,
        ISubtractionOperators<T, T, T>
{
    private readonly TimeSpan _duration;
    private readonly TimeSpan _delay;
    private readonly Curve _curve;
    private double _timePassedInSeconds; // = 0;

    public DurationAnimation(TimeSpan duration, Curve curve)
        : this(TimeSpan.Zero, duration, curve) { }

    public DurationAnimation(TimeSpan delay, TimeSpan duration, Curve curve)
    {
        _duration = duration;
        _delay = delay;
        _curve = curve;
    }

    public override ParametricAnimation<T1> ForParametr<T1>() =>
        new DurationAnimation<T1>(_delay, _duration, _curve);

    public override void Start(T start, T end)
    {
        _timePassedInSeconds = 0;
        base.Start(start, end);
    }

    protected override void EveryUpdate(XNA::GameTime gameTime)
    {
        _timePassedInSeconds += gameTime.ElapsedGameTime.TotalSeconds;
        double passedWithoutDelay = _timePassedInSeconds - _delay.TotalSeconds;
        if (passedWithoutDelay > 0)
        {
            double time = passedWithoutDelay / _duration.TotalSeconds;
            if (time > 1.0)
            {
                time = 1.0;
                IsComplited = true;
                Value = _end;
                _onUpdate?.Invoke(Value);
            }
            Value = _start + (_diffrence * _curve.GetValueAtTime(time));
            _onUpdate?.Invoke(Value);
        }
    }
}
