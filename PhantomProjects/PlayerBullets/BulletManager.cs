using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects.PlayerBullets
{
    class BulletManager
    {
        #region Definitions
        static Texture2D bulletTexture;
        static Rectangle bulletRectangle;
        static public List<Bullet> bullets;
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 200f;

        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

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

            if (p.currentAnim == p.playerRight)
            {
                bulletPosition.Y += 15;
                bulletPosition.X += 25;
            }
            else
            {
                bulletPosition.Y += 15;
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
                if (!bullets[i].Active || bullets[i].Position.X > 8000 || bullets[i].Position.X < -8000)
                {
                    bullets.Remove(bullets[i]);
                }
            }

            foreach (EnemyA e in EnemyManager.enemyType1)
            {
                foreach (Bullet B in BulletManager.bullets)
                {
                    bulletRectangle = new Rectangle(
                                      (int)B.Position.X,
                                      (int)B.Position.Y,
                                      B.Width,
                                      B.Height);

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

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (var b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
