using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dino2
{
    internal class Dino : Sprite
    {
        private int lives { get; set; }
        private float opacity { get; set; }
        
        private float gravity { get; set; }
        private float jumpVelocity { get; set; }
        private bool onGround { get; set; }
        private bool hasCollided { get; set; }
        private bool hasPassed { get; set; }
        private float velocityY;
        private int score { get; set; }

        private bool _spacePressed;

        public Dino(Texture2D texture, Vector2 position, GraphicsDevice graphicsDevice)
            : base(texture, position, graphicsDevice) // Pass GraphicsDevice to the base constructor
        {
            lives = 10;
            speed = 750f;
            gravity = 3600f;
            jumpVelocity = 1400f;
            onGround = true;
            velocityY = 0f;
            opacity = 0f;
            Width = 100;
            offset.X = 95;
            score = 0;
        }

        public void CollisionCheck(Cactus cactus)
        {
            if (!hasCollided && Rect.Intersects(cactus.Rect))
            {
                hasCollided = true;
                lives -= 1;
                opacity += 0.1f;
                cactus.RemoveSpeed();
            }

            if (cactus.position.X > graphicsDevice.Viewport.Width - 10)
            {
                hasCollided = false;
                hasPassed = false;
            }
        }
        public int GetLives()
        {
            return lives;
        }
        public float GetOpacity()
        {
            return opacity;
        }

        public void SetScore(Cactus cactus)
        {
            if (!hasPassed && !hasCollided && (cactus.position.X + cactus.Width) < position.X && (cactus.position.X + cactus.Width) > 0)
            {
                score += 1;
                hasPassed = true;
                cactus.AddSpeed();
            }
        }

        public int GetScore()
        {
            return score;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Apply gravity if Dino is not on the ground
            if (!onGround)
            {
                velocityY += gravity * deltaTime; // Increase downward velocity due to gravity
            }

            // Handle jumping
            if (!_spacePressed && Keyboard.GetState().IsKeyDown(Keys.Space) && onGround)
            {
                _spacePressed = true;
                velocityY = -jumpVelocity; // Apply upward velocity for jump
                onGround = false; // Dino is in the air now
            }

            // Release space key
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                _spacePressed = false;
            }

            // Apply vertical velocity (jump or fall)
            position.Y += velocityY * deltaTime;

            // Ground collision detection
            if (position.Y >= groundLevel)
            {
                position.Y = groundLevel; // Keep Dino on the ground
                velocityY = 0f;           // Stop falling
                onGround = true;          // Dino is back on the ground
            }
        }
    }
}
