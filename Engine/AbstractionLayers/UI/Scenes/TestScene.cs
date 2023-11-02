using Engine.SceneElements.Animations;
using Engine.SceneElements.Animations.Curves;
using Engine.SceneElements.Characteristics;
using Engine.SceneElements.Components;
using Engine.SceneElements.Components.Other;
using Engine.SceneElements.GameObjects;
using System.Collections.Generic;

namespace Engine.SceneElements.UI.Scenes;

public class TestScene : IUpdatable, IDrawable
{
    private List<Button> _buttons;
    private ButtonCursor _cursor;

    public bool IsVisible
    {
        get => true;
    }
    public bool IsActive
    {
        get => true;
    }

    public TestScene()
    {
        _buttons = new List<Button>();

        for (int i = 1; i < 4; i++)
        {
            Button button = new Button();
            TransformComponent transform = button.GetComponent<TransformComponent>();
            transform.Size = new Size() { Width = 100, Height = 100 };
            transform.Center = new Vector2(200, 150 * i);
            _buttons.Add(button);
        }
        _cursor = new ButtonCursor(
            new DurationAnimation<float>(new System.TimeSpan(0, 0, 0, 5), new Ease()),
            _buttons
        )
        {
            CheckInput = true
        };
        //_cursor.GetComponent<TransformComponent>().Position = new Vector3(200, 200, 0);
    }

    public void Draw(XNA.GameTime gameTime) => _cursor.Draw(gameTime);

    public void Update(XNA.GameTime gameTime) => _cursor.Update(gameTime);
}
