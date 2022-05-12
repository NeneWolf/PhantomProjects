using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using PhantomProjects.States;
using PhantomProjects.Player_;
using PhantomProjects.Map_;


namespace PhantomProjects.Map_
{
    class Platforms
    {
        #region Declarations
        Texture2D platformTexture;

        public Rectangle rectangle;
        public Vector2 position, velocity;
        Vector2 currentPosition, destinePosition;
        float movingDistance;

        bool Active;
        bool vertical;

        #endregion

        public int Width
        {
            get { return platformTexture.Width; }
        }
        public int Height
        {
            get { return platformTexture.Height; }
        }

        public void Initialize(Vector2 Position, ContentManager content, bool Horizontal, int MovingDistance, bool active)
        {
            platformTexture = content.Load<Texture2D>("Map\\Platform");

            position = Position;
            currentPosition = position;
            vertical = Horizontal;
            movingDistance = MovingDistance;
            Active = active;

            destinePosition.Y = position.Y - movingDistance;
            destinePosition.X = position.X + movingDistance;
        }

        public void Update(GameTime gameTime)
        {
            if(Active == true)
            {
                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
                PlatformMovement(gameTime);
            }
        }
        public bool UpdateStatus(bool activate) => Active = activate;

        void PlatformMovement(GameTime gameTime)
        {
            if (vertical == true)
            {
                if (currentPosition.Y == position.Y)
                {
                    velocity.Y -= 1.5f;
                }
                else if (position.Y <= destinePosition.Y)
                {
                    velocity.Y += 1.5f;
                }
            }
            else
            {
                if (currentPosition.X == position.X)
                {
                    velocity.X += 1.5f;
                }
                else if (position.X >= destinePosition.X)
                {
                    velocity.X -= 1.5f;
                }
            }
        }

        public bool VERTICAL()
        {
            return vertical;
        }

        public bool IsActive() { return Active; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(platformTexture, rectangle, Color.White);
        }
    }
}
