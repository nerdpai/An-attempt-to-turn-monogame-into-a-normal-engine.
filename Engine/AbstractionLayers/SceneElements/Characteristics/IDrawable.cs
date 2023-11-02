using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Engine.SceneElements.Characteristics;

public interface IDrawable
{
    protected static SpriteBatch _spriteBatch;

    public abstract bool IsVisible { get; }

    public static void SetSpriteBatch(SpriteBatch spriteBatch)
    {
        Debug.Assert(spriteBatch != null, $"parametr {nameof(spriteBatch)} should not be null.");
        _spriteBatch = spriteBatch;
    }

    public abstract void Draw(XNA::GameTime gameTime);
}
