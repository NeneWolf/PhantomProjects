using System;
using System.Collections.Generic;
using System.Text;
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
        #region Definitions
        static Texture2D bulletTextureRight, bulletTextureLeft;
        static Rectangle bulletRectangle;
        static public List<Bullet> bullets;
        const float SECONDS_IN_MINUTE = 100f;
        const float RATE_OF_FIRE = 100f;

        //public int damage;
        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        #endregion

        public void Initialize(ContentManager content)
        {
            bullets = new List<Bullet>();
            previousBulletSpawnTime = TimeSpan.Zero;
            bulletTextureRight = content.Load<Texture2D>("EnemyA\\EnemyBulletRight");
            bulletTextureLeft = content.Load<Texture2D>("EnemyA\\EnemyBulletLeft"); ;
        }

        private static void FireBullet(GameTime gameTime, Player p, Sounds SND, UpgradeMenu upgrade)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(p, upgrade);
                SND.BULLET.Play();
            }
        }

        private static void AddBullet(Player p, UpgradeMenu upgrade)
        {
            Animation bulletAnimation = new Animation();
            
            Bullet bullet = new Bullet();

            var bulletPosition = p.Position;

            //change bullet spawn position depending on the side
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

        public void UpdateManagerBullet(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND,Boss boss, UpgradeMenu upgrade)
        {
            // if input is pressed, player fire bullets
             if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed )
            {
                FireBullet(gameTime, p, SND, upgrade);
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
                        // Show the blood where the enemy is
                        VFX.AddExplosion(new Vector2(B.Position.X + 20, B.Position.Y -20), SND);

                        //Reduce enemy Health & disactivate bullet
                        e.Health -= B.Damage;
                        B.Active = false;
                    }
                }

                //Boss case
                if (boss != null && boss.Active == true)
                {
                    if (bulletRectangle.Intersects(boss.RECTANGLE))
                    {
                        // Show the blood where the boss is
                        VFX.AddExplosion(B.Position, SND);

                        //Reduce enemy Health & disactivate bullet
                        boss.Health -= B.Damage;
                        B.Active = false;
                    }
                }
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            //Bullet Colliding with Tiles get destroyed
            foreach(Bullet B in BulletManager.bullets)
            {
                if (bulletRectangle.TouchTopOf(newRectangle) || bulletRectangle.TouchLeftOf(newRectangle) ||
                bulletRectangle.TouchRightOf(newRectangle) || bulletRectangle.TouchBottomOf(newRectangle))
                {
                    B.Active = false;
                }
            }
        }

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (var b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
