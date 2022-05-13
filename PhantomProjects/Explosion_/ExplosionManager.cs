using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhantomProjects.Explosion_
{
    class ExplosionManager
    {
        #region Declarations

        // Collections of explosions
        List<Explosion> explosions;

        //Texture to hold explosion animation.
        Texture2D explosionTexture;

        //Handle Graphics info
        Vector2 graphicsInfo;

        #endregion

        #region Constructor
        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;

            // init list of blood explosions and texture
            explosions = new List<Explosion>();
            explosionTexture = texture;
        }
        #endregion

        #region Methods
        public void AddExplosion(Vector2 targetPosition, Sounds SND)
        {
            // ini the animation of the explotions
            Animation explosionAnimation = new Animation();

            //set the explosion rectangle based on the "target" position
            explosionAnimation.Initialize(explosionTexture, 
                targetPosition,
                100,
                100,
                10,
                35,
                Color.White,
                1.0f,
                true);

            //ini the explosion
            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, targetPosition);

            //Add explosion to the list
            explosions.Add(explosion);

            //Blood explosion sound
            SND.BLOOD.Play();
        }

        public void UpdateExplosions(GameTime gameTime)
        {
            //Update explostion and remove once they become Active = false
            for (var e = 0; e < explosions.Count; e++)
            {
                explosions[e].Update(gameTime);

                if (!explosions[e].Active)
                    explosions.Remove(explosions[e]);
            }
        }

        public void DrawExplosions(SpriteBatch spriteBatch)
        {
            // draw explosions
            foreach (var e in explosions)
            {
                e.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
