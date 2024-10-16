using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dino2
{
    internal class Cactus : Sprite
    {
        public Cactus(Texture2D texture, Vector2 position, GraphicsDevice graphicsDevice) : base(texture, position, graphicsDevice)
        {
            speed = 500;
        }

        public void AddSpeed()
        {
            speed += 10;
            //Debug.WriteLine(speed);
        }
        public void RemoveSpeed()
        {
            speed -= 25;
            //Debug.WriteLine(speed);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeRate;
            position.X -= speed * DeltaTime;
            if (position.X < 0 - Width)
            {
                position.X = graphicsDevice.Viewport.Width;
            }
        }
    }
}
