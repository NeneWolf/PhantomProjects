using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhantomProjects.Explosion_
{
    class Explosion
    {
        #region Declarations

        Animation explosionAnimation; //blood explosion animation
        Vector2 Position; // position of spawn
        public bool Active; 
        int timeToLive; // time that will be ran
        #endregion

        // Return dimentions of the explostion based on the explosion animation frame width/height
        public int Width
        {
            get { return explosionAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return explosionAnimation.FrameWidth; }
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            explosionAnimation = animation; // Set explosion animation
            Position = position; // Set explosion position
            Active = true;
            timeToLive = 30; //set the time of explosion life
        }

        public void Update(GameTime gameTime)
        {
            // update explosion
            explosionAnimation.Update(gameTime);

            // Reduce the explosion time and set active to false once its been finished
            timeToLive -= 1;

            if (timeToLive <= 0)
            {
                this.Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            explosionAnimation.Draw(spriteBatch);
        }
    }
}
