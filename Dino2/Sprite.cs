using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dino2
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public float groundLevel;
        public GraphicsDevice graphicsDevice;
        public float RawDeltaTime;
        public float DeltaTime;
        internal float speed { get; set; }
        public float TimeRate => 1;

        public int Width { get;  set; }  // Width of the sprite
        public int Height { get; private set; } // Height of the sprite

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)position.X + (int)offset.X, (int)position.Y+ (int)offset.Y, Width, Height);
            }
        }
        public Vector2 offset;


        public Sprite(Texture2D texture, Vector2 position, GraphicsDevice graphicsDevice)
        {
            this.texture = texture;
            this.position = position;
            this.graphicsDevice = graphicsDevice;

            // Store the texture dimensions
            Width = texture.Width;
            Height = texture.Height;

            // Calculate ground level based on texture height and screen height
            groundLevel = graphicsDevice.Viewport.Height - Height;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}