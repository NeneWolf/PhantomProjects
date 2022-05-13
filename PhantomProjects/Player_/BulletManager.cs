using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Boss_;
using PhantomProjects.Enemy_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;
using PhantomProjects.Menus_;

namespace PhantomProjects.Player_
{
    class BulletManager
    {
        #region Declarations
        static Texture2D bulletTextureRight, bulletTextureLeft;
        static Rectangle bulletRectangle;
        static public List<Bullet> bullets;
        const float SECONDS_IN_MINUTE = 100f;
        const float RATE_OF_FIRE = 100f;

        //public int damage;
        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;
        #endregion

        #region Constructor
        public void Initialize(ContentManager content)
        {
            //Create a list to hold all bullet objects
            bullets = new List<Bullet>();
            previousBulletSpawnTime = TimeSpan.Zero;

            //Load textures
            bulletTextureRight = content.Load<Texture2D>("EnemyA\\EnemyBulletRight");
            bulletTextureLeft = content.Load<Texture2D>("EnemyA\\EnemyBulletLeft"); ;
        }
        #endregion

        #region Methods
        //Method to shoot bullet
        private static void FireBullet(GameTime gameTime, Player p, Sounds SND, UpgradeMenu upgrade)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(p, upgrade);
                SND.BULLET.Play();
            }
        }

        //Method for creating the bullets
        private static void AddBullet(Player p, UpgradeMenu upgrade)
        {
            Animation bulletAnimation = new Animation();

            Bullet bullet = new Bullet();

            var bulletPosition = p.Position;

            //Change bullet direction depending on which way the player is facing
            if (p.currentAnim == p.playerRight || p.currentAnim == p.idleRight)
            {
                bulletAnimation.Initialize(bulletTextureRight, p.Position, 46, 16, 1, 30, Color.White, 1f, true);
                bulletPosition.Y += 40;
                bulletPosition.X += 25;
            }
            else
            {
                bulletAnimation.Initialize(bulletTextureLeft, p.Position, 46, 16, 1, 30, Color.White, 1f, true);
                bulletPosition.Y += 40;
                bulletPosition.X -= 25;
            }

            bullet.Initialize(bulletAnimation, bulletPosition, p, upgrade);
            bullets.Add(bullet);
        }

        //Update Method
        public void UpdateManagerBullet(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND, Boss boss, UpgradeMenu upgrade)
        {
            //If input button is pressed, fire bullets
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                FireBullet(gameTime, p, SND, upgrade);
            }

            for (var i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);

                //Remove bullets so they don't last forever
                if (!bullets[i].Active || bullets[i].Position.X > bullets[i].Position.X + 1280 ||
                    bullets[i].Position.X < bullets[i].Position.X - 1280)
                {
                    bullets.Remove(bullets[i]);
                }
            }

            //Collision between bullets and enemies
            foreach (Bullet B in BulletManager.bullets)
            {
                //Create a rectangle for each bullet to determine collision
                bulletRectangle = new Rectangle(
                                      (int)B.Position.X,
                                      (int)B.Position.Y,
                                      B.Width,
                                      B.Height);

                //Create a rectangle for each enemy to determine collision
                foreach (EnemyA e in EnemyManager.enemyType1)
                {
                    //Checks if the bullet and enemy rectangles collide
                    if (bulletRectangle.Intersects(e.rectangle))
                    {
                        //Create blood effect at the enemy position
                        VFX.AddExplosion(new Vector2(B.Position.X + 20, B.Position.Y - 20), SND);

                        //Reduce the enemy's health by the bullet's damage
                        e.Health -= B.Damage;

                        //Remove the bullet so it doesnt keep moving across the screen
                        B.Active = false;
                    }
                }

                //Same for the boss
                if (boss != null && boss.Active == true)
                {
                    if (bulletRectangle.Intersects(boss.RECTANGLE))
                    {
                        //Create blood effect at the enemy position
                        VFX.AddExplosion(B.Position, SND);

                        //Reduce the enemy's health by the bullet's damage
                        boss.Health -= B.Damage;

                        //Remove the bullet so it doesnt keep moving across the screen
                        B.Active = false;
                    }
                }
            }
        }

        //Check if bullet collides with a tile
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            //Bullets colliding with tiles get destroyed
            foreach (Bullet B in BulletManager.bullets)
            {
                if (bulletRectangle.TouchTopOf(newRectangle) || bulletRectangle.TouchLeftOf(newRectangle) ||
                bulletRectangle.TouchRightOf(newRectangle) || bulletRectangle.TouchBottomOf(newRectangle))
                {
                    B.Active = false;
                }
            }
        }

        //Draw Method
        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (var b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
