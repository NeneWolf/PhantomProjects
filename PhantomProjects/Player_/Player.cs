using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Map_;


namespace PhantomProjects.Player_
{
    class Player
    {
        public Animation playerAnimation;
        public Texture2D playerRight, playerLeft, idleRight, idleLeft, currentAnim;
        float elapsed;
        float delay = 120f, delayIdle = 800f;
        int Frames = 0;

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

        bool right;

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


        public void Initialize(ContentManager content, Vector2 newPosition, int playerSelected)
        {
            if(playerSelected == 0)
            {
                playerRight = content.Load<Texture2D>("Player\\FemalePlayerRightWalk");
                playerLeft = content.Load<Texture2D>("Player\\FemalePlayerLeftWalk");
                idleRight = content.Load<Texture2D>("Player\\FemaleRightIdle");
                idleLeft = content.Load<Texture2D>("Player\\FemaleLeftIdle");
            }
            else
            {
                playerRight = content.Load<Texture2D>("Player\\MalePlayerRightWalk");
                playerLeft = content.Load<Texture2D>("Player\\MalePlayerLeftWalk");
                idleRight = content.Load<Texture2D>("Player\\MaleRightIdle");
                idleLeft = content.Load<Texture2D>("Player\\MaleLeftIdle");
            }

            // Set the player to be active
            Active = true;
            // Set the player health
            Health = 100;

            // the size of the health bar... this will be used to cute the bar based on the damage 100 - 10d and 150 - 15d
            BarHealth = 150;

            position = newPosition;

            playerAnimation = new Animation();

            currentAnim = idleRight;
        }

        public void Update(GameTime gameTime)
        {
            IsDead();

            if(Active == true)
            {
                // Gamepad controls
                previousGamePadState = currentGamePadState;
                currentGamePadState = GamePad.GetState(PlayerIndex.One);

                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 100);
                playerAnimation.Position = position;
                playerAnimation.Update(gameTime);

                Input(gameTime);

                if (velocity.Y < 10)
                    velocity.Y += 0.35f;
            }
        }

        private void Input(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) ||
                currentGamePadState.DPad.Right == ButtonState.Pressed || currentGamePadState.ThumbSticks.Left.X == 1)
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnim = playerRight;
                Animate(gameTime);
                right = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)||
                currentGamePadState.DPad.Left == ButtonState.Pressed || currentGamePadState.ThumbSticks.Left.X == -1)
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnim = playerLeft;
                Animate(gameTime);
                right = false;
            }
            else
            {
                velocity.X = 0f;
                if (right == true)
                {
                    currentAnim = idleRight;
                    AnimateIdle(gameTime);
                }
                else
                {
                    currentAnim = idleLeft;
                    AnimateIdle(gameTime);
                }
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) ||
                currentGamePadState.Buttons.A == ButtonState.Pressed) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -11f;
                hasJumped = true;
            }
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

        public void ChangePositionOnPlatforms(float positionX, float positionY, bool Jump)
        {
            position.Y = positionY;
            position.X = positionX;
            hasJumped = Jump;
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

            sourceRect = new Rectangle((Frames * 50), 0, 50, 100);
        }

        void AnimateIdle(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delayIdle)
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

            sourceRect = new Rectangle((Frames * 50), 0, 50, 100);
        }


        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }

        private void IsDead()
        {
            if (Health <= 0)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }
    }
}
