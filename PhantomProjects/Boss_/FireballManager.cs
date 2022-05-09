using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.PlayerBullets;

namespace PhantomProjects.Boss_
{
    class FireballManager
    {
        static Texture2D fireballTexture;
        static Rectangle fireballRectangle;
        static public List<Fireball> fireball;
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 120f;

        static TimeSpan fireballSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        static Vector2 graphicsInfo;

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            fireball = new List<Fireball>();
            previousBulletSpawnTime = TimeSpan.Zero;
            fireballTexture = texture;

            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        public static void FireBulletE(GameTime gameTime, Boss b, Sounds SND)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > fireballSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(b);
                SND.BULLET.Play();
            }
        }

        private static void AddBullet(Boss b)
        {
            Animation fireballAnimation = new Animation();
            fireballAnimation.Initialize(fireballTexture, b.position, 50, 50, 1, 30, Color.White, 1f, true);

            Fireball fireball_ = new Fireball();

            var fireballPosition = b.position;
            Random r = new Random();
            int nextValue = r.Next((int)b.position.X - 300, (int)b.position.X + 300);

            fireballPosition.Y = b.position.Y - 448;
            fireballPosition.X = nextValue;

            fireball_.Initialize(fireballAnimation, fireballPosition);

            fireball.Add(fireball_);
        }

        public void UpdateManagerFireball(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND)
        {
            for (var i = 0; i < fireball.Count; i++)
            {
                fireball[i].Update(gameTime);

                if (!fireball[i].Active || fireball[i].Position.Y > 8000)
                {
                    fireball.Remove(fireball[i]);
                }
            }

            Rectangle playerRectangle = new Rectangle(
                    (int)p.Position.X,
                    (int)p.Position.Y,
                    p.Width,
                    p.Height
                );

            foreach (Fireball fire in FireballManager.fireball)
            {
                fireballRectangle = new Rectangle(
                    (int)fire.Position.X,
                    (int)fire.Position.Y,
                    fire.Width,
                    fire.Height
                    );

                if (fireballRectangle.Intersects(playerRectangle))
                {
                    // Show the explosion where the player was.
                    VFX.AddExplosion(p.Position, SND);

                    // Damage Player
                    p.Health -= fire.damage;
                    p.BarHealth -= 15;

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
