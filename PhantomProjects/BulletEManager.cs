using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects
{
    class BulletEManager
    {
        static Texture2D bulletETexture;
        static Rectangle bulletERectangle;
        static public List<BulletE> bulletEBeams;
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 120f;

        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        GraphicsDeviceManager graphics;
        static Vector2 graphicsInfo;

        static bool direction;

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            bulletEBeams = new List<BulletE>();
            previousBulletSpawnTime = TimeSpan.Zero;
            bulletETexture = texture;
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        public static void FireBulletE(GameTime gameTime, EnemyA e, bool Direction, Sounds SND)
        {
            direction = Direction;

            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(e);
                SND.BULLET.Play();
            }
        }

        private static void AddBullet(EnemyA e)
        {
            Animation bulletAnimation = new Animation();
            bulletAnimation.Initialize(bulletETexture, e.position, 46, 16, 1, 30, Color.White, 1f, true);
            BulletE bullet = new BulletE();

            var bulletPosition = e.position;
            if (!direction)
            {
                bulletPosition.Y += 17;
                bulletPosition.X -= 55;
            }
            else
            {
                bulletPosition.Y += 17;
                bulletPosition.X += 35;
            }

            bullet.Initialize(bulletAnimation, bulletPosition, direction);

            bulletEBeams.Add(bullet);
        }

        public void UpdateManagerBulletE(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND)
        {

            for (var i = 0; i < bulletEBeams.Count; i++)
            {
                bulletEBeams[i].Update(gameTime);

                if (!bulletEBeams[i].Active || bulletEBeams[i].Position.X > 1856 || bulletEBeams[i].Position.X < -1856)
                {
                    bulletEBeams.Remove(bulletEBeams[i]);
                }
            }

            Rectangle playerRectangle = new Rectangle(
                    (int)p.Position.X,
                    (int)p.Position.Y,
                    p.Width,
                    p.Height
                );

            foreach (BulletE b in BulletEManager.bulletEBeams)
            {
                bulletERectangle = new Rectangle(
                    (int)b.Position.X,
                    (int)b.Position.Y,
                    b.Width,
                    b.Height
                    );

                if (bulletERectangle.Intersects(playerRectangle))
                {
                    // Add explossion
                    // Show the explosion where the player was.
                    VFX.AddExplosion(p.Position, SND);

                    // Damage Player
                    p.Health -= b.damage;
                    p.BarHealth -= 15;

                    b.Active = false;

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
    }
}
