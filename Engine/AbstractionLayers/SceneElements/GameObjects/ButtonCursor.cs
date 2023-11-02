using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Engine.SceneElements.Components;
using Engine.SceneElements.Animations;
using Engine.SceneElements.Characteristics;
using Engine.SceneElements.Components.Other;

namespace Engine.SceneElements.GameObjects;

public class ButtonCursor : ComponentHandler, IUpdatable, IDrawable
{
    private readonly AnimatedTransformComponent _animTransform;
    private readonly List<Button> _buttons;
    private Button _currentButton;
    private static readonly Angle _fieldOfView = new() { InDegree = 100 };

    private readonly RectangleSprite _leftPlank;
    private readonly RectangleSprite _rightPlank;
    private readonly RectangleSprite _topPlank;
    private readonly RectangleSprite _bottomPlank;
    private Button CurrentButton
    {
        get => _currentButton;
        set
        {
            if (_currentButton != value)
            {
                if (_currentButton == null)
                {
                    TransformComponent transform = GetComponent<TransformComponent>();
                    TransformComponent buttonTransform = value.GetComponent<TransformComponent>();
                    transform.CopyFrom(buttonTransform);
                    transform.Scale = new Vector2();
                    _animTransform.Scale = buttonTransform.Scale;
                }
                else
                {
                    _animTransform.Scale = new Vector2();
                    if (value != null)
                        _animTransform.CopyFrom(value.GetComponent<TransformComponent>());
                }
                _currentButton = value;
            }
        }
    }

    public bool CheckInput { get; set; } //= false;
    public bool IsActive { get; private set; } = true;
    public bool IsVisible { get; private set; } = true;

    public ButtonCursor(Animation animation)
        : base(new AnimatedTransformComponent(animation))
    {
        const float procentedSize = 0.05f;
        Size defaultSize = new Size() { Width = 100f, Height = 100f };
        _currentButton = null;
        _animTransform = GetComponent<AnimatedTransformComponent>();
        TransformComponent transform = GetComponent<TransformComponent>();
        transform.Size = defaultSize;

        Size side =
            new()
            {
                Width = defaultSize.Width * procentedSize,
                Height = defaultSize.Height * (1f + procentedSize * 2f)
            };
        _leftPlank = new RectangleSprite(
            XNA::Color.White,
            side,
            new Vector3(-side.Width, -side.Width, 0f)
        );
        _rightPlank = new RectangleSprite(
            XNA::Color.White,
            side,
            new Vector3(defaultSize.Width, -side.Width, 0f)
        );

        Size floor =
            new()
            {
                Width = defaultSize.Width * (1f + procentedSize * 2f),
                Height = defaultSize.Height * procentedSize
            };
        _topPlank = new RectangleSprite(
            XNA::Color.White,
            floor,
            new Vector3(-floor.Height, -floor.Height, 0f)
        );
        _bottomPlank = new RectangleSprite(
            XNA::Color.White,
            floor,
            new Vector3(-floor.Height, defaultSize.Height, 0f)
        );

        AddChildren(_bottomPlank, _topPlank, _leftPlank, _rightPlank);

        _buttons = new();
    }

    public ButtonCursor(Animation animation, IEnumerable<Button> buttons)
        : this(animation) => AddRange(buttons);

    public ButtonCursor AddRange(params Button[] buttons) => AddRange((IEnumerable<Button>)buttons);

    public ButtonCursor AddRange(IEnumerable<Button> buttons)
    {
        Debug.Assert(
            buttons != null && !buttons.Any(b => b == null),
            $"parametr {nameof(buttons)} should not be or contain null."
        );

        foreach (var button in buttons)
            Add(button);

        return this;
    }

    public ButtonCursor Add(Button button)
    {
        Debug.Assert(button != null, $"parametr {nameof(button)} should not be null.");

        _buttons.Add(button);

        if (CurrentButton == null)
            CurrentButton = button;

        return this;
    }

    public void Update(XNA::GameTime gameTime)
    {
        if (IsActive)
        {
            _animTransform.Update(gameTime);
            if (CheckInput)
            {
                if (XNA::Input.Keyboard.GetState().IsKeyDown(XNA.Input.Keys.Up))
                    Move(Direction.Up);
                if (XNA::Input.Keyboard.GetState().IsKeyDown(XNA.Input.Keys.Down))
                    Move(Direction.Down);
                if (XNA::Input.Keyboard.GetState().IsKeyDown(XNA.Input.Keys.Left))
                    Move(Direction.Left);
                if (XNA::Input.Keyboard.GetState().IsKeyDown(XNA.Input.Keys.Right))
                    Move(Direction.Right);
            }
        }
    }

    public void Draw(XNA::GameTime gameTime)
    {
        if (IsVisible)
        {
            _leftPlank.Draw(gameTime);
            _rightPlank.Draw(gameTime);
            _topPlank.Draw(gameTime);
            _bottomPlank.Draw(gameTime);
        }
    }

    private void Move(Direction direction)
    {
        int comparer = direction is Direction.Down or Direction.Right ? 1 : -1;
        Button nextButton = _buttons
            .Where(button =>
            {
                Vector2 someButtonCenter = button.GetComponent<TransformComponent>().Center;
                Vector2 currentButtonCenter = CurrentButton
                    .GetComponent<TransformComponent>()
                    .Center;
                Vector2 difference = someButtonCenter - currentButtonCenter;
                Vector2 viewDirection = getViewDirection();

                return getVariableToCompare(difference).CompareTo(0f) == comparer
                    && Vector2.AngleBetween(difference, viewDirection).InDegree
                        < _fieldOfView.InDegree / 2f;
            })
            .DefaultIfEmpty()
            .MinBy(button =>
            {
                if (button == null)
                    return 0;
                Vector2 someButtonCenter = button.GetComponent<TransformComponent>().Center;
                Vector2 currentButtonCenter = CurrentButton
                    .GetComponent<TransformComponent>()
                    .Center;
                Vector2 difference = someButtonCenter - currentButtonCenter;
                return Math.Sqrt(difference.X * difference.X + difference.Y * difference.Y);
            });

        if (nextButton != null)
            CurrentButton = nextButton;

        float getVariableToCompare(Vector2 Center) =>
            direction is Direction.Down or Direction.Up ? Center.Y : Center.X;
        Vector2 getViewDirection() =>
            direction is Direction.Up or Direction.Down
                ? new Vector2(0, comparer)
                : new Vector2(comparer, 0);
    }

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }
}
