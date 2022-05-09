using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects.Boss_
{
    class Fireball
    {
        #region Declarations
        public Animation FireballAnimation;
        Texture2D fireballTexture, currentAnim;
        float elapsed;
        float delay = 120f;
        int Frames = 0;

        public Rectangle rectangle, sourceRect; // Canvas for texture movement

        float fireballMoveSpeed;
        public Vector2 Position;
        public int damage;
        public bool Active;


        public int Width
        {
            get { return FireballAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return FireballAnimation.FrameHeight; }
        }
        #endregion

        public void Initialize(Animation animation, Vector2 position, ContentManager content)
        {
            fireballTexture = content.Load<Texture2D>("Boss\\FireAnim");

            FireballAnimation = animation;
            currentAnim = fireballTexture;

            Position = position;
            Active = true;
            damage = 15;
            fireballMoveSpeed = 8f;
            
        }

        public void Update(GameTime gameTime)
        {
            if(Active == true)
            {
                Position.Y += fireballMoveSpeed;
                rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 50);

                FireballAnimation.Position = Position;
                FireballAnimation.Update(gameTime);
                Animate(gameTime);
            }
        }

        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 3)
                {
                    Frames = 0;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }

            sourceRect = new Rectangle((Frames * 50), 0, 50, 50);
        }

        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
        }
    }
}
