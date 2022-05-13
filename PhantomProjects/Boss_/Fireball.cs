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

        public Animation FireballAnimation; //Fireball animation
        Texture2D fireballTexture, currentAnim; //Fireball textures

        //Animation parameters
        float elapsed;
        float delay = 120f;
        int Frames = 0;

        public Rectangle rectangle, sourceRect; // Canvas for texture movement
        
        //Fireball Stats
        float fireballMoveSpeed; //fireball velocity
        public Vector2 Position; //position
        public int damage; //damage that can inflict 
        public bool Active;

        #endregion

        // Return dimentions of the fireball based on the fireball animation frame width/height
        public int Width
        {
            get { return FireballAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return FireballAnimation.FrameHeight; }
        }

        public void Initialize(Animation animation, Vector2 position, ContentManager content)
        {
            //Initialise fireball content
            fireballTexture = content.Load<Texture2D>("Boss\\FireAnim");

            //Set animation texture & initialise Animation
            FireballAnimation = animation;
            currentAnim = fireballTexture;

            //Set position
            Position = position;

            //Set fireball Stats
            Active = true;
            damage = 15;
            fireballMoveSpeed = 8f;
            
        }

        public void Update(GameTime gameTime)
        {
            // if Fireball is active then create canvas and initialise animation
            if(Active == true)
            {
                //If active, set the velocity of the fireball & rectangle
                Position.Y += fireballMoveSpeed;
                rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 50);

                //Set animation position to the fireball position & Update
                FireballAnimation.Position = Position;
                FireballAnimation.Update(gameTime);
                Animate(gameTime);
            }
        }

        //Animation Methods
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

        //Returns the Fireball rectangle
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
