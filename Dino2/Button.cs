using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dino2
{
    internal class Button : Sprite
    {
        public Color color;
        public bool pressed;
        public Button(Texture2D texture, Vector2 position, GraphicsDevice graphicsDevice) : base(texture, position, graphicsDevice)
        {
            color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Rectangle mousePosition = new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 1, 1);
            if (mousePosition.Intersects(Rect))
            {
                color = Color.DarkGray;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    pressed = true;
                }
            }
            else
            {
                color = Color.White;
                pressed = false;
            }
        }
    }
}
