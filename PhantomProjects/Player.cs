﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace PhantomProjects
{
    class Player
    {
        public Animation playerAnimation;
        Texture2D playerRight, playerLeft, currentAnim;
        float elapsed;
        float delay = 120f;
        int Frames = 0;



        //Hold the Viewport
        Vector2 graphicsInfo;

        // Position of the Player relative to the upper left side of the screen
        private Vector2 position;

        private Vector2 velocity;
        public Rectangle rectangle, sourceRect;

        // State of the player
        public bool Active;
        // Amount of hit points that player has
        public int Health, BarHealth;

        // Jump
        private bool hasJumped = false;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        // Get the width of the player ship
        public int Width
        {
            get { return playerAnimation.FrameWidth; }
        }
        // Get the height of the player ship
        public int Height
        {
            get { return playerAnimation.FrameHeight; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void Initialize(Texture2D PlayerRight, Texture2D PlayerLeft, Vector2 newPosition)
        {
            // Set the player to be active
            Active = true;
            // Set the player health
            Health = 100;

            // the size of the health bar... this will be used to cute the bar based on the damage 100 - 10d and 150 - 15d
            BarHealth = 150;

            position = newPosition;

            playerAnimation = new Animation();

            playerRight = PlayerRight;
            playerLeft = PlayerLeft;

            currentAnim = playerRight;
        }

        public void Update(GameTime gameTime)
        {
            IsDead();
            if(Active == true)
            {
                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, 64, 64);
                playerAnimation.Position = position;
                playerAnimation.Update(gameTime);

                Input(gameTime);

                if (velocity.Y < 10)
                    velocity.Y += 0.4f;
            }
        }

        private void Input(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnim = playerRight;
                Animate(gameTime);

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnim = playerLeft;
                Animate(gameTime);
            }
            else
            {
                velocity.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -10f;
                hasJumped = true;
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }
        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 6)
                {
                    Frames = 0;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle((Frames * 64), 0, 64, 64);
        }

        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }


        private void IsDead() { if (Health <= 0) { Active = false; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
        }
    }
}
