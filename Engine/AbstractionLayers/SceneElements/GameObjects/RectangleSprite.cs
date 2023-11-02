using Engine.SceneElements.Characteristics;
using Engine.SceneElements.Components;
using Engine.SceneElements.Components.Other;

namespace Engine.SceneElements.GameObjects;

public class RectangleSprite : ComponentHandler, IDrawable
{
    private static readonly XNA::Graphics.Texture2D _texture =
        ResourceManager.Resource.Load<XNA::Graphics.Texture2D>("Rectangle");
    public bool IsVisible
    {
        get => true;
    }

    public XNA::Color Color { get; set; }

    public RectangleSprite(XNA::Color color)
        : base(new TransformComponent()) => Color = color;

    public RectangleSprite(XNA::Color color, Size size)
        : base(new TransformComponent() { Size = size }) => Color = color;

    public RectangleSprite(XNA::Color color, Size size, Vector3 position)
        : base(new TransformComponent() { Size = size, Position = position }) => Color = color;

    public void Draw(XNA.GameTime gameTime)
    {
        if (IsVisible)
        {
            TransformComponent transform = GetComponent<TransformComponent>();
            IDrawable._spriteBatch.Draw(
                _texture,
                (Vector2)transform.ScaledPosition,
                null,
                this.Color,
                transform.Rotation.InRadians,
                XNA::Vector2.Zero,
                (Vector2)transform.ScaledSize,
                XNA::Graphics.SpriteEffects.None,
                transform.ScaledPosition.Z
            );
        }
    }
}
