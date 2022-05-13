using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomProjects.Player_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;
using Microsoft.Xna.Framework.Content;

namespace PhantomProjects.Enemy_
{
    class BulletEManager
    {
        #region Declarations

        static Texture2D bulletETextureRight, bulletETextureLeft; //E bullet texture
        static Vector2 graphicsInfo; // call for the graphics Information
        static Rectangle bulletERectangle; // bullet canvas
        static public List<BulletE> bulletEBeams; //create a bullet list

        //Cals for the fireball spawn
        const float SECONDS_IN_MINUTE = 105f;
        const float RATE_OF_FIRE = 115f;

        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        static bool direction;
        #endregion

        #region Construtor
        public void Initialize(ContentManager content, GraphicsDevice Graphics)
        {
            // Create the new list
            bulletEBeams = new List<BulletE>();

            //Set the previous bullet spawwn to 0
            previousBulletSpawnTime = TimeSpan.Zero;

            //Retrieve and save the game graphics information
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;

            //Assign texture
            bulletETextureRight = content.Load<Texture2D>("EnemyA\\EnemyBulletRight");
            bulletETextureLeft = content.Load<Texture2D>("EnemyA\\EnemyBulletLeft"); ;
        }
        #endregion

        #region Methods
        // Method that will initialise each bullet
        public static void FireBulletE(GameTime gameTime, EnemyA e, bool Direction, Sounds SND)
        {
            //Set the bullet direction
            direction = Direction;

            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                // Assign new value to the previous Bullet spawn
                previousBulletSpawnTime = gameTime.TotalGameTime;

                //Call the function to initialize the new bullet
                AddBullet(e);

                //Call the function to initialize the new bullet
                SND.BULLET.Play();
            }
        }

        private static void AddBullet(EnemyA e)
        {
            // Initialise the bullet animation & the bullet itself
            Animation bulletAnimation = new Animation();
            
            BulletE bullet = new BulletE();

            // set the bullet position of spawning to the enemy position
            var bulletPosition = e.position;

            //  Depending on the direction that the enemy is facing, change the spawn and sprite of the bullet
            if (!direction)
            {
                bulletAnimation.Initialize(bulletETextureLeft, e.position, 46, 16, 1, 30, Color.White, 1f, true);
                bulletPosition.Y += 22;
                bulletPosition.X -= 1;
            }
            else
            {
                bulletAnimation.Initialize(bulletETextureRight, e.position, 46, 16, 1, 30, Color.White, 1f, true);
                bulletPosition.Y += 22;
                bulletPosition.X += 55;
            }

            // Inicialise the bullet itself with the values set previously
            bullet.Initialize(bulletAnimation, bulletPosition, direction);

            //Add bullet to the list
            bulletEBeams.Add(bullet);
        }

        public void UpdateManagerBulletE(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND)
        {
            // Update each bullet
            for (var i = 0; i < bulletEBeams.Count; i++)
            {
                bulletEBeams[i].Update(gameTime);

                // Remove any bullet that is out of range our inactive
                if (!bulletEBeams[i].Active || bulletEBeams[i].Position.X > 8000 || bulletEBeams[i].Position.X < -8000)
                {
                    bulletEBeams.Remove(bulletEBeams[i]);
                }
            }

            // for each bullet in the list, create the canvas 
            foreach (BulletE b in BulletEManager.bulletEBeams)
            {
                bulletERectangle = new Rectangle(
                    (int)b.Position.X,
                    (int)b.Position.Y,
                    b.Width,
                    b.Height
                    );

                //Check if any bullet has intersected with the player rectangle
                if (bulletERectangle.Intersects(p.RECTANGLE))
                {
                    //create blood explosion on the player position
                    VFX.AddExplosion(p.Position, SND);

                    // Damage Player
                    p.Health -= b.damage;
                    p.BarHealth -= 15;

                    //set bullet to inactive
                    b.Active = false;

                }
            }

        }

        //Return bullet rectangle
        public Rectangle RECTANGLE()
        {
            return bulletERectangle;
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            // Check if any bullet has intersect map tiles and disable them
            foreach (BulletE B in BulletEManager.bulletEBeams)
            {
                if (bulletERectangle.TouchTopOf(newRectangle) || bulletERectangle.TouchLeftOf(newRectangle) ||
                bulletERectangle.TouchRightOf(newRectangle) || bulletERectangle.TouchBottomOf(newRectangle))
                {
                    B.Active = false;
                }
            }
        }

        public void DrawBullet(SpriteBatch spriteBatch)
        {
            foreach (var b in bulletEBeams)
            {
                b.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
