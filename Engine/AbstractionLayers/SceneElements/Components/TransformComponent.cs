using System;
using System.Diagnostics;
using System.Collections.Generic;
using Engine.SceneElements.Components.Other;
using Engine.SceneElements.Components.Characteristics;

namespace Engine.SceneElements.Components;

public class TransformComponent
    : BindableComponent<TransformComponent>,
        ICopyable<TransformComponent>
{
    private readonly Dictionary<TransformComponent, Action<Size, Size>> _sizeBindings = new();
    public event Action<Size, Size> OnSizeChanged;

    protected Size _size = new() { Width = 100f, Height = 100f };
    public Size Size
    {
        get => _size;
        set
        {
            Debug.Assert(
                value.Width > 0f,
                $"parametr {nameof(value.Width)} should be in (0, infinity) range."
            );
            Debug.Assert(
                condition: value.Height > 0f,
                $"parametr {nameof(value.Height)} should be in (0, infinity) range."
            );

            OnSizeChanged?.Invoke(_size, value);
            _size = value;
        }
    }

    private readonly Dictionary<TransformComponent, Action<Vector3, Vector3>> _positionBindings =
        new();
    public event Action<Vector3, Vector3> OnPositionChanged;

    protected Vector3 _position = new();
    public Vector3 Position
    {
        get => _position;
        set
        {
            Debug.Assert(
                value.Z is >= 0f and <= 1f,
                $"parametr {nameof(Position.Z)} should be in [0, 1] range."
            );
            OnPositionChanged?.Invoke(_position, value);
            _position = value;
        }
    }

    private readonly Dictionary<TransformComponent, Action<Angle, Angle>> _rotationBindings = new();
    public event Action<Angle, Angle> OnRotationChanged;

    protected Angle _rotation = new();
    public Angle Rotation
    {
        get => _rotation;
        set
        {
            OnRotationChanged?.Invoke(_rotation, value);
            _rotation = value;
        }
    }

    private readonly Dictionary<TransformComponent, Action<Vector2, Vector2>> _scaleBindings =
        new();
    public event Action<Vector2, Vector2> OnScaleChanged;

    protected Vector2 _scale = new(1f, 1f);
    public Vector2 Scale
    {
        get => _scale;
        set
        {
            Debug.Assert(
                value.X >= 0f,
                $"parametr {nameof(value.X)} should be in [0, infinity) range."
            );
            Debug.Assert(
                condition: value.Y >= 0f,
                $"parametr {nameof(value.Y)} should be in [0, infinity) range."
            );

            OnScaleChanged?.Invoke(_scale, value);
            _scale = value;
        }
    }

    public Vector2 Center
    {
        get => (Vector2)(Position / 2f);
        set => Position = new Vector3(value - (Size / 2f), Position.Z);
    }

    public Size ScaledSize => Size * Scale;
    public Vector3 ScaledPosition
    {
        get
        {
            Vector2 difference = Size - ScaledSize;
            return Position + (Vector3)difference;
        }
    }
    public Vector2 ScaledCenter => (Vector2)(ScaledPosition / 2f);

    public override void Bind(TransformComponent bindItToMyself)
    {
        // csharpier-ignore
        void onPos(Vector3 oldValue, Vector3 newValue) =>
            bindItToMyself.Position += newValue - oldValue;

        OnPositionChanged += onPos;
        _positionBindings[bindItToMyself] = onPos;

        void onSize(Size oldValue, Size newValue)
        {
            Vector2 offset = (newValue / oldValue - 1f) * (bindItToMyself.Center - Center);

            //bindItToMyself.Center += offset;
            bindItToMyself.Size *= newValue / oldValue;
        }
        OnSizeChanged += onSize;
        _sizeBindings[bindItToMyself] = onSize;
        // csharpier-ignore


        void onScale(Vector2 oldValue, Vector2 newValue)
        {
            if (oldValue.X != 0 && oldValue.Y != 0)
            bindItToMyself.Scale = newValue / oldValue * bindItToMyself.Scale;
            else
            bindItToMyself.Scale = newValue;
        }

        OnScaleChanged += onScale;
        _scaleBindings[bindItToMyself] = onScale;

        void onRotation(Angle oldValue, Angle newValue)
        {
            bindItToMyself.Rotation += newValue - oldValue;
            //bindItToMyself.Position = if i would like to use it
        }
        OnRotationChanged += onRotation;
        _rotationBindings[bindItToMyself] = onRotation;
    }

    public override void Unbind(TransformComponent unbindItFromMyself)
    {
        OnScaleChanged -= _scaleBindings[unbindItFromMyself];
        OnPositionChanged -= _positionBindings[unbindItFromMyself];
        OnSizeChanged -= _sizeBindings[unbindItFromMyself];
        OnRotationChanged -= _rotationBindings[unbindItFromMyself];

        _scaleBindings.Remove(unbindItFromMyself);
        _positionBindings.Remove(unbindItFromMyself);
        _sizeBindings.Remove(unbindItFromMyself);
        _scaleBindings.Remove(unbindItFromMyself);
    }

    public void CopyFrom(TransformComponent from)
    {
        Scale = from.Scale;
        Position = from.Position;
        Rotation = from.Rotation;
        Size = from.Size;
    }
}
