using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;


namespace PhantomProjects.Boss_
{
    class FireballManager
    {
        #region Declarations

        static ContentManager fireContent; // Call of ContentManager to be passed to the Fireball functions to add the Texture
        static Vector2 graphicsInfo; // call for the graphics Infor to spawn the fireball within a range
        static Rectangle fireballRectangle; //Fireball canvas
        static public List<Fireball> fireball; // Create a fireball list

        //Cals for the fireball spawn
        const float SECONDS_IN_MINUTE = 35f; 
        const float RATE_OF_FIRE = 120f;

        static TimeSpan fireballSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        #endregion

        public void Initialize(GraphicsDevice Graphics, ContentManager content)
        {
            // Create the new list
            fireball = new List<Fireball>();

            //Set the previous bullet spawwn to 0
            previousBulletSpawnTime = TimeSpan.Zero;

            // set ContentManager to the fireContent
            fireContent = content;

            //Retrieve and save the game graphics information
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        // Method that will initialise each fireball
        public static void FireBulletE(GameTime gameTime, Boss b, Sounds SND)
        {
            //iif the difference between to the total game time - previous bullet shot is higher then fireball spawn, then create new fireball
            if (gameTime.TotalGameTime - previousBulletSpawnTime > fireballSpawnTime)
            {
                // Assign new value to the previous Bullet spawn
                previousBulletSpawnTime = gameTime.TotalGameTime;
                
                //Call the function to initialize the new fireball
                AddBullet(b, fireContent);

                //Add Sound to the spawn of the fireball
                SND.FIREBALL.Play();
            }
        }

        private static void AddBullet(Boss b, ContentManager content)
        {
            // Initialise the Fireball animation & the Fireball itself
            Animation fireballAnimation = new Animation();
            Fireball fireball_ = new Fireball();

            // set the fireball position of spawning to the boss position
            var fireballPosition = b.position;

            // Generate a random number , within the range of the boss +850 -850
            // No matter where the boss is, this value will be always calculate from the boss origin to +850 to -850
            Random r = new Random();
            int nextValue = r.Next((int)b.position.X - 850, (int)b.position.X + 850);

            fireballPosition.Y = b.position.Y - 380; // set the fireball position Y to 380 higher from the boss position 
            fireballPosition.X = nextValue; // set the fireball position X to the random value generated previously 

            fireball_.Initialize(fireballAnimation, fireballPosition, content); // Inicialise the fireball itself with the values set previously

            fireball.Add(fireball_); // Add this new fireball into the list
        }

        public void UpdateManagerFireball(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND)
        {
            for (var i = 0; i < fireball.Count; i++)
            {
                // Update each fireball inside the list
                fireball[i].Update(gameTime);

                if (!fireball[i].Active)
                {
                    // Remove any fireball that has its Active set as false, meaning that they died
                    fireball.Remove(fireball[i]);
                }
            }

            foreach (Fireball fire in FireballManager.fireball)
            {
                // Go over each fireball within the list and check if it has intersected with the player rectangle
                if (fire.rectangle.Intersects(p.RECTANGLE))
                {
                    // show blood explotion if it has & initiate blood explotion sound
                    VFX.AddExplosion(p.Position, SND);

                    // Damage Player
                    p.Health -= fire.damage;
                    p.BarHealth -= 15;

                    // set this specific fireball as inactive
                    fire.Active = false;
                }
            }

        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            // check each fireball , and if they have touched any map tile, set them as inactive
            foreach (Fireball fire in FireballManager.fireball)
            {
                if (fireballRectangle.TouchTopOf(newRectangle) || fireballRectangle.TouchLeftOf(newRectangle) ||
                fireballRectangle.TouchRightOf(newRectangle) || fireballRectangle.TouchBottomOf(newRectangle))
                {
                    fire.Active = false;
                }
            }
        }


        public void DrawFireball(SpriteBatch spriteBatch)
        {
            foreach (var b in fireball)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
