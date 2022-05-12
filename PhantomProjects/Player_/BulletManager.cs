using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Boss_;
using PhantomProjects.Enemy_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;

namespace PhantomProjects.Player_
{
    class BulletManager
    {
        #region Definitions
        static Texture2D bulletTexture;
        static Rectangle bulletRectangle;
        static public List<Bullet> bullets;
        const float SECONDS_IN_MINUTE = 100f;
        const float RATE_OF_FIRE = 100f;

        //public int damage;
        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        #endregion

        public void Initialize(Texture2D texture)
        {
            bullets = new List<Bullet>();
            previousBulletSpawnTime = TimeSpan.Zero;
            bulletTexture = texture;
        }

        private static void FireBullet(GameTime gameTime, Player p, Sounds SND)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(p);
                SND.BULLET.Play();
            }
        }

        private static void AddBullet(Player p)
        {
            Animation bulletAnimation = new Animation();
            bulletAnimation.Initialize(bulletTexture, p.Position, 46, 16, 1, 30, Color.White, 1f, true);
            Bullet bullet = new Bullet();

            var bulletPosition = p.Position;

            if (p.currentAnim == p.playerRight || p.currentAnim == p.idleRight)
            {
                bulletPosition.Y += 50;
                bulletPosition.X += 25;
            }
            else
            {
                bulletPosition.Y += 50;
                bulletPosition.X -= 25;
            }

            bullet.Initialize(bulletAnimation, bulletPosition, p);
            bullets.Add(bullet);
        }

        public void UpdateManagerBullet(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND)
        {
             if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed )
            {
                FireBullet(gameTime, p, SND);
            }

            for (var i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);

                if (!bullets[i].Active || bullets[i].Position.X > bullets[i].Position.X+ 1280 || 
                    bullets[i].Position.X < bullets[i].Position.X - 1280)
                {
                    bullets.Remove(bullets[i]);
                }
            }


            foreach (Bullet B in BulletManager.bullets)
            {
                bulletRectangle = new Rectangle(
                                      (int)B.Position.X,
                                      (int)B.Position.Y,
                                      B.Width,
                                      B.Height);

                foreach (EnemyA e in EnemyManager.enemyType1)
                {
                    if (bulletRectangle.Intersects(e.rectangle))
                    {
                        // Show the explosion where the player was.
                        VFX.AddExplosion(B.Position, SND);

                        e.Health -= B.Damage;
                        B.Active = false;
                    }
                }
            }
        }

        public void UpdateBullet(GameTime gameTime, Player p, Boss boss, ExplosionManager VFX, Sounds SND)
        {
            if(boss.Active == true)
            {
                foreach (Bullet B in BulletManager.bullets)
                {
                    bulletRectangle = new Rectangle(
                      (int)B.Position.X,
                      (int)B.Position.Y,
                      B.Width,
                      B.Height);

                    if (bulletRectangle.Intersects(boss.RECTANGLE))
                    {
                        // Show the explosion where the player was.
                        VFX.AddExplosion(B.Position, SND);

                        boss.Health -= B.Damage;
                        B.Active = false;
                    }
                }
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            foreach(Bullet B in BulletManager.bullets)
            {
                if (bulletRectangle.TouchTopOf(newRectangle) || bulletRectangle.TouchLeftOf(newRectangle) ||
                bulletRectangle.TouchRightOf(newRectangle) || bulletRectangle.TouchBottomOf(newRectangle))
                {
                    B.Active = false;
                }
            }
        }

        //public int ReturnBulletDamage() { return damage; }

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (var b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
