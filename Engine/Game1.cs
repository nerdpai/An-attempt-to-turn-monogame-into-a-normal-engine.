using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.SceneElements.GameObjects;
using Engine.SceneElements.UI.Scenes;
using System;

namespace Engine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TestScene _scene;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //_graphics.IsFullScreen = false;
        //_graphics.PreferredBackBufferWidth = 640;
        //_graphics.PreferredBackBufferHeight = 480;
        //_graphics.ApplyChanges();
        Console.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ResourceManager.SetManager(Content);
        SceneElements.Characteristics.IDrawable.SetSpriteBatch(_spriteBatch);

        _scene = new TestScene();
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        _scene.Update(gameTime);
        // TODO: Add your update logic here
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        // var rec = new RectangleSprite(
        //     Color.White,
        //     new Engine.SceneElements.Components.Other.Size() { Width = 100, Height = 100 },
        //     new Engine.SceneElements.Components.Other.Vector3(100, 100, 0)
        // );
        // rec.Draw(gameTime);

        _scene.Draw(gameTime);

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
