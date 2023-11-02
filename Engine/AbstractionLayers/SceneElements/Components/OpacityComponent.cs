using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Engine.SceneElements.Components;

public class OpacityComponent : BindableComponent<OpacityComponent>
{
    private readonly Dictionary<OpacityComponent, Action<float, float>> _opacityBindings = new();
    public event Action<float, float> OnOpacutyChanged;
    protected float _opacity = 1.0f;
    public float Opacity
    {
        get => _opacity;
        set
        {
            Debug.Assert(
                value is >= 0.0f and <= 1.0f,
                $"parametr {nameof(Opacity)} should be in [0, 1] range."
            );
            OnOpacutyChanged?.Invoke(_opacity, value);
            _opacity = value;
        }
    }

    public override void Bind(OpacityComponent bindItToMyself)
    {
        // csharpier-ignore
        void temp(float oldValue, float newValue) =>
            bindItToMyself.Opacity *= newValue / oldValue;

        OnOpacutyChanged += temp;
        _opacityBindings[bindItToMyself] = temp;
    }

    public override void Unbind(OpacityComponent unbindItFromMyself)
    {
        OnOpacutyChanged -= _opacityBindings[unbindItFromMyself];
        _opacityBindings.Remove(unbindItFromMyself);
    }
}
