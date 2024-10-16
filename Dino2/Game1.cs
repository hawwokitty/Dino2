using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dino2
{

    enum Scenes
    {
        MENU,
        GAME,
        GAMEOVER,
    };
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Cactus cactus;
        private Dino dino;
        private SpriteFont font;
        private Button startButton;
        private Scenes activeScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            activeScene = Scenes.MENU;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            Texture2D cactus_texture = Content.Load<Texture2D>("cactus");
            Texture2D dino_texture = Content.Load<Texture2D>("dino");
            Texture2D start_texture = Content.Load<Texture2D>("start");

            // Calculate the ground level based on the viewport height and texture heights
            float dinoGroundLevel = GraphicsDevice.Viewport.Height - dino_texture.Height;
            float spriteGroundLevel = GraphicsDevice.Viewport.Height - cactus_texture.Height;
            float offScreen = GraphicsDevice.Viewport.Width;

            // Set the Y position to the ground level for both Dino and Sprite
            dino = new Dino(dino_texture, new Vector2(100, dinoGroundLevel), GraphicsDevice);
            cactus = new Cactus(cactus_texture, new Vector2(offScreen, spriteGroundLevel), GraphicsDevice);
            startButton = new Button(start_texture, new Vector2(20, 50), GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/gameFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (activeScene)
            {
                case Scenes.MENU:
                    startButton.Update(gameTime);
                    if (startButton.pressed)
                    {
                        activeScene = Scenes.GAME;
                        startButton.pressed = false;
                    }
                    break;
                case Scenes.GAME:
                    dino.Update(gameTime);
                    cactus.Update(gameTime);

                    dino.CollisionCheck(cactus);
                    dino.SetScore(cactus);

                    int lives = dino.GetLives();
                    if (lives <= 0)
                    {
                        activeScene = Scenes.GAMEOVER;
                    }
                    break;
                case Scenes.GAMEOVER:
                    //gameover
                    break;
            }

            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            int lives = dino.GetLives();
            int score = dino.GetScore();
            float opacity = dino.GetOpacity();
            switch (activeScene)
            {
                case Scenes.MENU:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(startButton.texture, startButton.position, startButton.color);
                    _spriteBatch.End();
                    break;
                case Scenes.GAME:

                    // TODO: Add your drawing code here
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(cactus.texture, cactus.position, Color.White);
                    _spriteBatch.Draw(dino.texture, dino.position, Color.White);
                    _spriteBatch.Draw(dino.texture, dino.position, Color.Red * opacity);
                    //_spriteBatch.Draw(DrawRectangle(dino.Width, dino.Height, Color.Gold), dino.Rect, Color.White);
                    _spriteBatch.DrawString(font, $"HP: {lives}, Score: {score}", Vector2.Zero, Color.White);
                    _spriteBatch.End();
                    break;
                case Scenes.GAMEOVER:
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(font, $"GAME OVER, you score was {score}", Vector2.Zero, Color.Red);
                    _spriteBatch.End();
                    break;
            }



            base.Draw(gameTime);
        }

        public Texture2D DrawRectangle(int width, int height, Color color)
        {
            Texture2D rect = new Texture2D(GraphicsDevice, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            return rect;
        }
    }
}
